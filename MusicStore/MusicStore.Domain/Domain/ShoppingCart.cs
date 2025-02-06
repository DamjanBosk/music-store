namespace MusicStore.Domain.Domain
{
    using MusicStore.Domain.Identity;

    public class ShoppingCart : BaseEntity
    {
        public required string OwnerId { get; set; }
        public MusicStoreUser? Owner { get; set; }
        public virtual ICollection<AlbumInShoppingCart>? AlbumsInShoppingCart { get; set; } = null;
    }
}
