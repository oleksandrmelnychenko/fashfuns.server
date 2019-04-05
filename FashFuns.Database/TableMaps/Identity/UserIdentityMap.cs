using FashFuns.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashFuns.Database.TableMaps.Identity
{
    public class UserIdentityMap : EntityBaseMap<UserIdentity>
    {
        public override void Map(EntityTypeBuilder<UserIdentity> entity)
        {
            base.Map(entity);
            entity.ToTable("UserIdentities");
        }
    }
}
