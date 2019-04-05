using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashFuns.Database.TableMaps.Product
{
    public class UserProductMapMap : EntityBaseMap<Domain.Entities.Products.UserProductMap>
    {
        public override void Map(EntityTypeBuilder<Domain.Entities.Products.UserProductMap> entity)
        {
            base.Map(entity);
            entity.ToTable("UserProductMaps");
        }
    }
}
