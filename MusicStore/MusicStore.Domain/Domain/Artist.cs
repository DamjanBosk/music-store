namespace MusicStore.Domain.Domain
{
    public class Artist : BaseEntity
    {
        public required string Name { get; set; }
        public string? Bio { get; set; }
        public string? Country { get; set; }
        public virtual ICollection<Album>? Albums { get; set; }
    }
}
