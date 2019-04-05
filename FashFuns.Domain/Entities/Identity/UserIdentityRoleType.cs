namespace FashFuns.Domain.Entities.Identity
{
    public class UserIdentityRoleType : EntityBase
    {
        public RoleType RoleType { get; set; }

        public string Name { get; set; }
    }
}
