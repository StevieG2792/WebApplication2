using WebApplication2.DBContexts;
using WebApplication2.Models.Products;

namespace WebApplication2.ProductRepository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts(int page);
        void InsertProduct (ProductBase product);
        void UpdateProduct (Product product);
        void DeleteProduct (int productId);
        Product GetProductByID(int productId);
        void Save();
    }
}
