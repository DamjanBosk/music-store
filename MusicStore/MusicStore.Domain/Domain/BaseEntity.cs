namespace MusicStore.Domain.Domain
{
    using System.ComponentModel.DataAnnotations;
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
