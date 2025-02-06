namespace MusicStore.Web.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using MusicStore.Service.Interface;

    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartsController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        // GET: ShoppingCarts
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

            var model = _shoppingCartService.getShoppingCartDetails(userId ?? "");

            if (model.AllAlbums == null)
            {
                return RedirectToAction("EmptyCart", "ShoppingCarts");
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteAlbumFromShoppingCart(Guid? albumId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

            var result = _shoppingCartService.deleteFromShoppingCart(userId, albumId);

            return RedirectToAction("Index", "ShoppingCarts");
        }

        public async Task<IActionResult> Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

            var result = _shoppingCartService.orderAlbums(userId ?? "");

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction("SuccessOrder", "ShoppingCarts");
        }

        public IActionResult SuccessOrder()
        {
            return View();
        }

        public IActionResult EmptyCart()
        {
            return View();
        }
    }
}
