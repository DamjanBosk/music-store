namespace MusicStore.Repository.Interface
{
    using MusicStore.Domain.Identity;

    public interface IUserRepository
    {
        IEnumerable<MusicStoreUser> GetAll();
        MusicStoreUser Get(string id);
        void Insert(MusicStoreUser entity);
        void Update(MusicStoreUser entity);
        void Delete(MusicStoreUser entity);
    }
}
