namespace MusicStore.Domain.Domain
{
    public class AlbumInOrder : BaseEntity
    {
        public required Guid AlbumId { get; set; }
        public virtual Album? Album { get; set; }
        public required Guid OrderId { get; set; }
        public virtual Order? Order { get; set; }
        public int Quantity { get; set; }
    }
}
