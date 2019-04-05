using FashFuns.Domain.Entities.Identity;

namespace FashFuns.Domain.Repositories.Identity.Contracts
{
    public interface IIdentityRepository
    {
        UserIdentity GetUserByEmail(string email);

        int UpdateUserPassword(UserIdentity user);

        long UpdateUserExperationDate(long userId, bool isExpired);

        UserIdentity NewUser(string name, string email, string passwordHash, string passwordSalt);
    }
}
