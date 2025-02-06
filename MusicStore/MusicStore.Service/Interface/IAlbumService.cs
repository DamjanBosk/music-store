namespace MusicStore.Service.Interface
{
    using MusicStore.Domain.Domain;

    public interface IAlbumService
    {
        public List<Album> GetAll();
        public Album GetById(Guid? id);
        public Album Create(Album album);
        public Album Update(Album album);
        public Album Delete(Guid? id);
    }
}
