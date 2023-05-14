using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DBContexts;
using WebApplication2.Models.Products;

namespace WebApplication2.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _dbContext;

        public ProductRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult DeleteProduct(int productId)
        {
            var product = _dbContext.Products.Find(productId);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                Save();
                return new OkObjectResult("Product deleted: " + productId);
            }

            return new BadRequestObjectResult("No Product with this Id exists: " + productId);
           
        }

        public Product GetProductByID(int productId)
        {
            return _dbContext.Products.Find(productId);
        }

        public IEnumerable<Product> GetProducts(int page)
        {
            int RecordsToSkip = (page -1) * 2; //As 2 records per page
            return _dbContext.Products.OrderBy(b => b.Id).Skip(RecordsToSkip).Take(2).ToList();
        }

        public void InsertProduct(ProductBase product)
        {
            _dbContext.Add(product);
            Save();

        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public IActionResult UpdateProduct(Product product)
        {
            var existCheck = _dbContext.Products.Find(product.Id);
            if (existCheck != null)
            {
                _dbContext.Entry(product).State = EntityState.Modified;
                Save();
                return new OkObjectResult("Product has been updated Id: " + product.Id);
            }
            return new BadRequestObjectResult("No product exists with this Id: " + product.Id);
        }

    }
}
