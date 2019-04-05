namespace FashFuns.Domain.DataContracts.Identity
{
    public class NewUserDataContract
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public long[] RoleIds { get; set; }
    }
}
