using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashFuns.Database.TableMaps.Product
{
    public class ShoppingCartMap : EntityBaseMap<Domain.Entities.Products.ShoppingCart>
    {
        public override void Map(EntityTypeBuilder<Domain.Entities.Products.ShoppingCart> entity)
        {
            base.Map(entity);
            entity.ToTable("ShoppingCarts");
        }
    }
}
