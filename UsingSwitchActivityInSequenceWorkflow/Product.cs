using System;
using System.ComponentModel;

namespace UsingSwitchActivityInSequenceWorkflow
{
    [TypeConverter(typeof(ProductConverter))]
    public class Product
    {
        public string ProductName { get; set; }
        public Guid ProductId { get; set; }

        public Product()
        {
            ProductName = "Defualt name";
            ProductId = Guid.NewGuid();
        }

        public Product(string productName, Guid productId)
        {
            ProductName = productName;
            ProductId = productId;
        }

        public override bool Equals(object obj)
        {
            var product = obj as Product;
            return product != null && String.Equals(ProductId, product.ProductId);
        }

        public override int GetHashCode()
        {
            return ProductName != null ? ProductName.GetHashCode() : 0;
        }
    }
}