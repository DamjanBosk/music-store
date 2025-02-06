namespace MusicStore.Domain.Identity
{
    using Microsoft.AspNetCore.Identity;
    using MusicStore.Domain.Domain;

    public class MusicStoreUser : IdentityUser
    {
        public required string FirstName { get; set; } = string.Empty;
        public required string LastName { get; set; } = string.Empty;
        public virtual ShoppingCart? UserShoppingCart { get; set; }
    }
}
