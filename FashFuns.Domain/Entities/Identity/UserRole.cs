namespace FashFuns.Domain.Entities.Identity {
    public class UserRole : EntityBase
    {
        public UserIdentityRoleType UserRoleType { get; set; }

        public virtual UserIdentity UserIdentity { get; set; }

        public long UserIdentityId { get; set; }
    }
}
