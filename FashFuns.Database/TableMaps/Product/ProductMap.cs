using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashFuns.Database.TableMaps.Product
{
    public class ProductMap : EntityBaseMap<Domain.Entities.Products.Product>
    {
        public override void Map(EntityTypeBuilder<Domain.Entities.Products.Product> entity)
        {
            base.Map(entity);
            entity.ToTable("Products");
        }
    }
}
