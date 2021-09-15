using Shop.API.Entities;
using Shop.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.API.Services
{
    public interface IProductRepository
    {
        /// CONTRACT(CRUD)

        //Create
        void AddProduct(Product product);

        //Read
        IEnumerable<Product> GetProducts();
        Product GetProduct(Guid id);
        IEnumerable<Product> GetProducts(ProductsResourceParameters productsResourceParameters);
        IEnumerable<Product> GetProducts(IEnumerable<Guid> productIds);

        //Update
        void UpdateProduct();

        //Delete
        void DeleteProduct(Guid id);

        //Validation
        bool ProductExists(Guid id);
        bool Save();
    }
}
