using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using FashFuns.Domain.Entities.Identity;
using FashFuns.Domain.Repositories.Identity.Contracts;

namespace FashFuns.Domain.Repositories.Identity
{
    public class IdentityRolesRepository : IIdentityRolesRepository
    {
        private readonly IDbConnection _connection;

        public IdentityRolesRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public List<UserRole> AssignRoles(long userId, List<long> roles)
        {
            List<UserRole> userRolesToReturn = new List<UserRole>();

            foreach (long roleId in roles)
            {
                UserRole userRole = _connection.Query<UserRole>(
                    "INSERT INTO [UserRoles] (IsDeleted, UserIdentityId, UserRoleTypeId) " +
                    "VALUES(0,@UserIdentityId,@UserRoleTypeId) " +
                    "SELECT * FROM [UserRoles] WHERE ID = (SELECT SCOPE_IDENTITY()) "
                    , new { UserIdentityId = userId, UserRoleTypeId = roleId }
                ).SingleOrDefault();

                userRolesToReturn.Add(userRole);
            }

            return userRolesToReturn;
        }
    }
}
