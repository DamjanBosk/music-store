namespace MusicStore.Service.Implementation
{
    using MusicStore.Domain.Domain;
    using MusicStore.Repository.Interface;
    using MusicStore.Service.Interface;

    public class AlbumService : IAlbumService
    {
        private readonly IRepository<Album> _albumRepository;

        public AlbumService(IRepository<Album> albumRepository, IRepository<Artist> artistRepository)
        {
            this._albumRepository = albumRepository;
        }

        public List<Album> GetAll()
        {
            return _albumRepository.GetAll().ToList();
        }

        public Album GetById(Guid? id)
        {
            return _albumRepository.Get(id);
        }

        public Album Create(Album album)
        {
            return _albumRepository.Insert(album);
        }

        public Album Update(Album album)
        {
            return _albumRepository.Update(album);
        }

        public Album Delete(Guid? id)
        {
            var album = this.GetById(id);
            return _albumRepository.Delete(album);
        }
    }
}
