using System;
using System.Collections.Generic;
using System.Text;
using FashFuns.Domain.Entities.Identity;

namespace FashFuns.Domain.Repositories.Identity.Contracts
{
    public interface IIdentityRolesRepository
    {
        List<UserRole> AssignRoles(long userId, List<long> roles);
    }
}
