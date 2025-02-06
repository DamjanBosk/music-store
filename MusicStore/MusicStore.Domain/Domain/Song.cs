namespace MusicStore.Domain.Domain
{
    using MusicStore.Domain.Enum;

    public class Song : BaseEntity
    {
        public required string Title { get; set; }
        public Genre? Genre { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public required Guid AlbumId { get; set; }
        public virtual Album? Album { get; set; }
    }
}
