using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FashFuns.Domain.Entities.Products
{
    public class ProductCategory : EntityBase
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }

        public string Category { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
