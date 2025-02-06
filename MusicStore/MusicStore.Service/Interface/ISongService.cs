namespace MusicStore.Service.Interface
{
    using MusicStore.Domain.Domain;

    public interface ISongService
    {
        public List<Song> GetAll();
        public List<Song> GetAllByAlbumId(Guid? albumId);
        public Song GetById(Guid? id);
        public Song Create(Song song);
        public Song Update(Song song);
        public Song Delete(Guid? id);
    }
}
