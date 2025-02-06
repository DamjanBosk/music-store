namespace MusicStore.Repository
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MusicStore.Domain.Domain;
    using MusicStore.Domain.Identity;

    public class ApplicationDbContext : IdentityDbContext<MusicStoreUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<Album> Album { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<AlbumInShoppingCart> AlbumInShoppingCart { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<AlbumInOrder> AlbumInOrder { get; set; }

    }
}
