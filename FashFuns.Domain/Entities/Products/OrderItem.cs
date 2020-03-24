using System;
using System.Collections.Generic;
using System.Text;

namespace FashFuns.Domain.Entities.Products
{
    public class OrderItem : EntityBase
    {
        public long ProductId { get; set; }

        public Product Product { get; set; }

        public double Qty { get; set; }

        public long OrderId { get; set; }
        public Order Order { get; set; }
    }
}
