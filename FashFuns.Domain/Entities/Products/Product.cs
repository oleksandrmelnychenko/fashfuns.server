namespace FashFuns.Domain.Entities.Products
{
    public class Product : EntityBase
    {
        public string Description { get; set; }

        public int Rate { get; set; }

        public decimal Price { get; set; }

        public long ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public string ImageSource { get; set; }
    }
}
