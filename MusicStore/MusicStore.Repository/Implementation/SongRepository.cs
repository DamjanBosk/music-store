using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.Domain;
using MusicStore.Repository.Interface;

namespace MusicStore.Repository.Implementation
{
    public class SongRepository : ISongRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Song> songs;

        public SongRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.songs = context.Set<Song>();
        }

        public IEnumerable<Song> GetAllByAlbumId(Guid? albumId)
        {
            if (albumId == Guid.Empty)
            {
                throw new ArgumentException("AlbumId cannot be empty.", nameof(albumId));
            }

            return songs.Where(song => song.AlbumId == albumId).ToList();
        }
    }
}
