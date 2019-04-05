using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashFuns.Database.TableMaps.Product
{
    public class ProductCategoryMap : EntityBaseMap<Domain.Entities.Products.ProductCategory>
    {
        public override void Map(EntityTypeBuilder<Domain.Entities.Products.ProductCategory> entity)
        {
            base.Map(entity);
            entity.ToTable("ProductCategories");
        }
    }
}
