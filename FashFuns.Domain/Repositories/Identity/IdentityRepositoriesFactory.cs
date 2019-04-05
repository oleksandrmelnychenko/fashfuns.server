using System.Data;
using FashFuns.Domain.Repositories.Identity.Contracts;

namespace FashFuns.Domain.Repositories.Identity
{
    public class IdentityRepositoriesFactory : IIdentityRepositoriesFactory
    {
        public IIdentityRepository NewIdentityRepository(IDbConnection connection) =>
            new IdentityRepository(connection);

        public IIdentityRolesRepository NewIdentityRolesRepository(IDbConnection connection) =>
            new IdentityRolesRepository(connection);
    }
}
