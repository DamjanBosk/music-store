namespace MusicStore.Web
{
    using Microsoft.AspNetCore.Mvc;
    using MusicStore.Domain.Domain;
    using MusicStore.Service.Interface;

    public class SongsController : Controller
    {
        private readonly ISongService songService;
        private readonly IAlbumService albumService;

        public SongsController(ISongService songService, IAlbumService albumService)
        {
            this.songService = songService;
            this.albumService = albumService;
        }

        // GET: Songs/5
        public async Task<IActionResult> FromAlbum(Guid? albumId)
        {
            if (albumId == null)
            {
                return NotFound();
            }

            var album = albumService.GetById(albumId);
            if (album == null)
            {
                return NotFound();
            }

            ViewBag.AlbumId = album.Id;
            ViewBag.AlbumTitle = album.Title;

            return View(songService.GetAllByAlbumId(albumId));
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = songService.GetById(id);

            if (song == null)
            {
                return NotFound();
            }

            var album = albumService.GetById(song.AlbumId);
            song.Album = album;

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create(Guid? albumId)
        {
            //ViewData["AlbumId"] = new SelectList(_context.Album, "Id", "Title");

            if (albumId == null)
            {
                return NotFound();
            }

            var album = albumService.GetById(albumId);
            if (album == null)
            {
                return NotFound();
            }

            ViewBag.AlbumId = album.Id;
            ViewBag.AlbumTitle = album.Title;

            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Genre,ReleaseDate,AlbumId,Id")] Song song)
        {
            var album = albumService.GetById(song.AlbumId);

            if (album == null)
            {
                return NotFound();
            }

            song.Album = album;

            if (ModelState.IsValid)
            {
                songService.Create(song);
                return RedirectToAction(nameof(Details), new { id = song.Id });
            }

            return View(song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = songService.GetById(id);
            if (song == null)
            {
                return NotFound();
            }

            var album = albumService.GetById(song.AlbumId);
            if (album == null)
            {
                return NotFound();
            }

            ViewBag.AlbumId = album.Id;
            ViewBag.AlbumTitle = album.Title;

            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Genre,ReleaseDate,AlbumId,Id")] Song song)
        {
            if (id != song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                songService.Update(song);
                return RedirectToAction(nameof(Details), new { id = song.Id });
            }
            return View(song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = songService.GetById(id);
            if (song == null)
            {
                return NotFound();
            }

            var album = albumService.GetById(song.AlbumId);
            song.Album = album;

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var song = songService.GetById(id);
            if (song == null)
            {
                return NotFound();
            }

            var album = albumService.GetById(song.AlbumId);
            if (album == null)
            {
                return NotFound();
            }

            songService.Delete(id);

            return RedirectToAction(nameof(FromAlbum), new { albumId = album.Id });
        }
    }
}
