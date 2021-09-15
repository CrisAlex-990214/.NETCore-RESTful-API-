using Shop.API.Entities;
using Shop.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.API.Services
{
    public class ProductRepository : IProductRepository
    {
        //Db context reference
        private readonly ProductRepositoryContext context;

        public ProductRepository(ProductRepositoryContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //Add a new Product 
        public void AddProduct(Product product)
        {
            product.id = Guid.NewGuid();
            context.products.Add(product);
        }

        public void DeleteProduct(Guid id)
        {
            context.products.Remove(GetProduct(id));
        }

        public Product GetProduct(Guid productId)
        {
            return context.products.FirstOrDefault(a => a.id == productId);
        }

        // Get all the products in the shop
        public IEnumerable<Product> GetProducts()
        {
            return context.products;
        }

        public IEnumerable<Product> GetProducts(ProductsResourceParameters productsResourceParameters)
        {   
            return context.products
                .Where(d => d.description.Contains(productsResourceParameters.description)
                        || d.type.Contains(productsResourceParameters.type))
                .ToList();
        }

        public IEnumerable<Product> GetProducts(IEnumerable<Guid> productIds)
        {
            return context.products.Where(a => productIds.Contains(a.id));
        }

        public bool ProductExists(Guid productId)
        {
            return context.products.Any(a => a.id == productId);
        }

        public bool Save()
        {
            return (context.SaveChanges() >= 0);
        }

        public void UpdateProduct()
        {
            Save();
        }
    }
}
