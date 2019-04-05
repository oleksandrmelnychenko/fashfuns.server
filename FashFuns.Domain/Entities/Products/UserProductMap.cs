using FashFuns.Domain.Entities.Identity;

namespace FashFuns.Domain.Entities.Products
{
    public class UserProductMap : EntityBase
    {
        public long UserIdentityId { get; set; }

        public UserIdentity UserIdentity { get; set; }

        public long ProductId { get; set; }

        public Product Product { get; set; }
    }
}
