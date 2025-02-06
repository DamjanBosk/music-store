using MusicStore.Domain.Domain;
using MusicStore.Domain.DTO;

namespace MusicStore.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCart AddAlbumToShoppingCart(string userId, AddToCartDTO model);
        AddToCartDTO getAlbumInfo(Guid Id);
        ShoppingCartDTO getShoppingCartDetails(string userId);
        Boolean deleteFromShoppingCart(string userId, Guid? Id);
        Boolean orderAlbums(string userId);
    }

}
