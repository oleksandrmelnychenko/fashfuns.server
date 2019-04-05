using FashFuns.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashFuns.Database.TableMaps.Product
{
    public class OrderMap : EntityBaseMap<Order>
    {
        public override void Map(EntityTypeBuilder<Order> entity)
        {
            base.Map(entity);
            entity.ToTable("Orders");
        }
    }
}
