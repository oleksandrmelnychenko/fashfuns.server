
namespace FashFuns.Domain.DataContracts.Identity
{
    public class ResetPasswordDataContract : AuthenticationDataContract
    {
        public string NewPassword { get; set; }
    }
}
