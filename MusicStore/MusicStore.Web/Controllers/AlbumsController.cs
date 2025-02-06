namespace MusicStore.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MusicStore.Domain.Domain;
    using MusicStore.Domain.DTO;
    using MusicStore.Service.Implementation;
    using MusicStore.Service.Interface;
    using System.Security.Claims;

    public class AlbumsController : Controller
    {
        private readonly IAlbumService albumService;
        private readonly IArtistService artistService;
        private readonly IShoppingCartService shoppingCartService;

        public AlbumsController(IAlbumService albumService, IArtistService artistService, IShoppingCartService shoppingCartService)
        {
            this.albumService = albumService;
            this.artistService = artistService;
            this.shoppingCartService = shoppingCartService;
        }

        // GET: Albums
        public async Task<IActionResult> Index()
        {
            List<Album> albums = albumService.GetAll();

            foreach(Album album in albums)
            {
                var artist = artistService.GetById(album.ArtistId);
                album.Artist = artist;
            }

            return View(albums);
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = albumService.GetById(id);

            if (album == null)
            {
                return NotFound();
            }

            var artist = artistService.GetById(album.ArtistId);
            album.Artist = artist;

            return View(album);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            List<Artist> artists = artistService.GetAll();
            ViewBag.Artists = new SelectList(artists, "Id", "Name");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Price,ReleaseDate,ArtistId,Id")] Album album)
        {
            if (ModelState.IsValid)
            {
                albumService.Create(album);
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = albumService.GetById(id);
            if (album == null)
            {
                return NotFound();
            }

            List<Artist> artists = artistService.GetAll();
            ViewBag.Artists = new SelectList(artists, "Id", "Name");

            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Price,ReleaseDate,ArtistId,Id")] Album album)
        {
            if (id != album.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                albumService.Update(album);
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = albumService.GetById(id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            albumService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddAlbumToCart(Guid Id)
        {
            var result = shoppingCartService.getAlbumInfo(Id);
            if (result != null)
            {
                return View(result);
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddAlbumToCart(AddToCartDTO model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = shoppingCartService.AddAlbumToShoppingCart(userId, model);

            if (result != null)
            {
                return RedirectToAction("Index", "ShoppingCarts");
            }
            else { return View(model); }
        }

    }
}
