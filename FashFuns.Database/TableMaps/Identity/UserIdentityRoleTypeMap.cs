using FashFuns.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashFuns.Database.TableMaps.Identity
{
    public class UserIdentityRoleTypeMap : EntityBaseMap<UserIdentityRoleType>
    {
        public override void Map(EntityTypeBuilder<UserIdentityRoleType> entity)
        {
            base.Map(entity);
            entity.ToTable("UserIdentityRoleTypes");
        }
    }
}
