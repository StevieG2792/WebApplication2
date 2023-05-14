using Microsoft.AspNetCore.Mvc;
using WebApplication2.DBContexts;
using WebApplication2.Models.Products;

namespace WebApplication2.ProductRepository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts(int page);
        void InsertProduct (ProductBase product);
        IActionResult UpdateProduct (Product product);
        IActionResult DeleteProduct (int productId);
        Product GetProductByID(int productId);
        void Save();
    }
}
