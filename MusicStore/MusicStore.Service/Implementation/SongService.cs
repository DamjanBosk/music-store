namespace MusicStore.Service.Implementation
{
    using MusicStore.Domain.Domain;
    using MusicStore.Repository.Interface;
    using MusicStore.Service.Interface;

    public class SongService : ISongService
    {
        private readonly IRepository<Song> repository;
        private readonly ISongRepository songRepository;

        public SongService(IRepository<Song> repository, ISongRepository songRepository)
        {
            this.repository = repository;
            this.songRepository = songRepository;
        }

        public List<Song> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public List<Song> GetAllByAlbumId(Guid? albumId)
        {
            return songRepository.GetAllByAlbumId(albumId).ToList();
        }

        public Song GetById(Guid? id)
        {
            return repository.Get(id);
        }

        public Song Create(Song song)
        {
            return repository.Insert(song);
        }

        public Song Update(Song song)
        {
            return repository.Update(song);
        }

        public Song Delete(Guid? id)
        {
            var song = this.GetById(id);
            return repository.Delete(song);
        }
    }
}
