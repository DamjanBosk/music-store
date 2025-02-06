namespace MusicStore.Domain.Domain
{
    using MusicStore.Domain.Identity;

    public class Order : BaseEntity
    {
        public required string OwnerId { get; set; }
        public MusicStoreUser? Owner { get; set; }
        public virtual ICollection<AlbumInOrder>? AlbumsInOrder { get; set; }
    }
}
