using System;
using System.Collections.Generic;
using System.Text;
using FashFuns.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashFuns.Database.TableMaps.Product
{
    public class OrderItemMap : EntityBaseMap<OrderItem>
    {
        public override void Map(EntityTypeBuilder<OrderItem> entity)
        {
            base.Map(entity);
            entity.ToTable("OrderItems");
        }
    }
}
