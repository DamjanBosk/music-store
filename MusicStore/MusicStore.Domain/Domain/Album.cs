namespace MusicStore.Domain.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class Album : BaseEntity
    {
        public required string Title { get; set; }
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Price cannot be negative")]
        public required decimal Price { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public required Guid ArtistId { get; set; }
        public virtual Artist? Artist { get; set; }
        public virtual ICollection<Song>? Songs { get; set; }
        public virtual ICollection<AlbumInShoppingCart>? AlbumsInShoppingCart { get; set; }
        public virtual ICollection<AlbumInOrder>? AlbumsInOrder { get; set; }
    }
}
