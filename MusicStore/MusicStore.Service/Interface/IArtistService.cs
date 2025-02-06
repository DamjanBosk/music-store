namespace MusicStore.Service.Interface
{
    using MusicStore.Domain.Domain;

    public interface IArtistService
    {
        public List<Artist> GetAll();
        public Artist GetById(Guid? id);
        public Artist Create(Artist artist);
        public Artist Update(Artist artist);
        public Artist Delete(Guid? id);
    }
}
