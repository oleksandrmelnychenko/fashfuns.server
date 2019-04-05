using FashFuns.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashFuns.Database.TableMaps.Identity
{
    public class UserRoleMap : EntityBaseMap<UserRole>
    {
        public override void Map(EntityTypeBuilder<UserRole> entity)
        {
            base.Map(entity);
            entity.ToTable("UserRoles");
        }
    }
}
