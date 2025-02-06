namespace MusicStore.Repository.Interface
{
    using MusicStore.Domain.Domain;

    public interface ISongRepository
    {
        IEnumerable<Song> GetAllByAlbumId(Guid? albumId);
    }
}
