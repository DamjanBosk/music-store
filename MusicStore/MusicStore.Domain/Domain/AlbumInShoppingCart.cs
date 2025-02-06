namespace MusicStore.Domain.Domain
{
    public class AlbumInShoppingCart : BaseEntity
    {
        public required Guid AlbumId { get; set; }
        public virtual Album? Album { get; set; }
        public required Guid ShoppingCartId { get; set; }
        public virtual ShoppingCart? ShoppingCart { get; set; }
        public int Quantity { get; set; }
    }
}
