namespace MusicStore.Service.Implementation
{
    using Microsoft.IdentityModel.Tokens;
    using MusicStore.Domain.Domain;
    using MusicStore.Domain.DTO;
    using MusicStore.Domain.Email;
    using MusicStore.Repository.Interface;
    using MusicStore.Service.Interface;
    using System.Text;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Album> _albumRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<AlbumInOrder> _albumInOrderRepository;
        private readonly IEmailService _emailService;

        public ShoppingCartService(IUserRepository userRepository, IRepository<ShoppingCart> shoppingCartRepository, IRepository<Album> albumRepository, IRepository<Order> orderRepository, IRepository<AlbumInOrder> albumInOrderRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _albumRepository = albumRepository;
            _orderRepository = orderRepository;
            _albumInOrderRepository = albumInOrderRepository;
            _emailService = emailService;
        }

        public ShoppingCart AddAlbumToShoppingCart(string userId, AddToCartDTO model)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userCart = loggedInUser?.UserShoppingCart;

                var selectedAlbum = _albumRepository.Get(model.SelectedAlbumId);

                if (userCart == null)
                {
                    var newUserCart = new ShoppingCart
                    {
                        Owner = loggedInUser,
                        OwnerId = loggedInUser?.Id ?? throw new InvalidOperationException("User ID is required")
                    };

                    _shoppingCartRepository.Insert(newUserCart);
                    userCart = newUserCart;
                }

                if (selectedAlbum != null && userCart != null)
                {
                    userCart?.AlbumsInShoppingCart?.Add(new AlbumInShoppingCart
                    {
                        Album = selectedAlbum,
                        AlbumId = selectedAlbum.Id,
                        ShoppingCart = userCart,
                        ShoppingCartId = userCart.Id,
                        Quantity = model.Quantity
                    });

                    return _shoppingCartRepository.Update(userCart);
                }
            }
            return null;
        }

        public bool deleteFromShoppingCart(string userId, Guid? Id)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var product_to_delete = loggedInUser?.UserShoppingCart?.AlbumsInShoppingCart.First(z => z.AlbumId == Id);

                loggedInUser?.UserShoppingCart?.AlbumsInShoppingCart?.Remove(product_to_delete);

                _shoppingCartRepository.Update(loggedInUser.UserShoppingCart);

                return true;

            }

            return false;
        }

        public AddToCartDTO getAlbumInfo(Guid Id)
        {
            var selectedAlbum = _albumRepository.Get(Id);
            if (selectedAlbum != null)
            {
                var model = new AddToCartDTO
                {
                    SelectedAlbumName = selectedAlbum.Title,
                    SelectedAlbumId = selectedAlbum.Id,
                    Quantity = 1
                };
                return model;
            }
            return null;
        }

        public ShoppingCartDTO getShoppingCartDetails(string userId)
        {
            if (userId != null && !userId.IsNullOrEmpty())
            {
                var loggedInUser = _userRepository.Get(userId);

                var allAlbums = loggedInUser?.UserShoppingCart?.AlbumsInShoppingCart?.ToList();

                decimal totalPrice = 0;

                if (allAlbums != null)
                {
                    foreach (var item in allAlbums)
                    {
                        totalPrice += Decimal.Round((item.Quantity * item.Album.Price), 2);
                    }
                }

                var model = new ShoppingCartDTO
                {
                    AllAlbums = allAlbums,
                    TotalPrice = totalPrice
                };

                return model;

            }

            return new ShoppingCartDTO
            {
                AllAlbums = new List<AlbumInShoppingCart>(),
                TotalPrice = 0
            };
        }

        public bool orderAlbums(string userId)
        {
            if (userId != null && !userId.IsNullOrEmpty())
            {
                var loggedInUser = _userRepository.Get(userId);

                var userCart = loggedInUser?.UserShoppingCart;

                EmailMessage message = new EmailMessage();
                message.Subject = "Successfull order";
                message.MailTo = loggedInUser.Email;

                var userOrder = new Order
                {
                    Id = Guid.NewGuid(),
                    OwnerId = userId,
                    Owner = loggedInUser
                };

                _orderRepository.Insert(userOrder);

                var albumInOrders = userCart?.AlbumsInShoppingCart.Select(z => new AlbumInOrder
                {
                    Order = userOrder,
                    OrderId = userOrder.Id,
                    AlbumId = z.AlbumId,
                    Album = z.Album,
                    Quantity = z.Quantity
                }).ToList();

                StringBuilder sb = new StringBuilder();

                decimal totalPrice = 0;

                sb.AppendLine("Your order is completed. The order conatins: ");

                for (int i = 1; i <= albumInOrders.Count(); i++)
                {
                    var currentItem = albumInOrders[i - 1];
                    totalPrice += currentItem.Quantity * currentItem.Album.Price;
                    sb.AppendLine(i.ToString() + ". " + currentItem.Album.Title + " with quantity of: " + currentItem.Quantity + " and price of: $" + currentItem.Album.Price);
                }

                sb.AppendLine("Total price for your order: " + totalPrice.ToString());
                message.Content = sb.ToString();

                _albumInOrderRepository.InsertMany(albumInOrders);

                userCart?.AlbumsInShoppingCart.Clear();

                _shoppingCartRepository.Update(userCart);

                this._emailService.SendEmailAsync(message);

                return true;
            }
            return false;
        }
    }
}
