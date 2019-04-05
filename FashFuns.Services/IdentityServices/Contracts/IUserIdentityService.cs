using System.Threading.Tasks;
using FashFuns.Domain.DataContracts.Identity;

namespace FashFuns.Services.IdentityServices.Contracts
{
    public interface IUserIdentityService
    {
        Task<UserAccount> SignInAsync(AuthenticationDataContract authenticateDataContract);

        Task<UserAccount> SignUpAsync(AuthorizationDataContract authenticateDataContract);
    }
}
