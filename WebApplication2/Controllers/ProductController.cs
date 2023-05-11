using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using WebApplication2.Models;
using WebApplication2.ProductRepository;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet ("GetProductByPage")]
        public IActionResult GetProductList(int page)
        {
            var products = _productRepository.GetProducts(page);
            return new OkObjectResult(products);
        }

        [HttpGet("GetProductById")]
        public IActionResult GetProductById(int id)
        {
            var product = _productRepository.GetProductByID(id);
            return new OkObjectResult(product);
        }

        [HttpPost("CreateProduct")]
        public IActionResult Post([FromBody] ProductBase productBase)
        {
            using (var scope = new TransactionScope())
            {
                Product product = new()
                {
                    Name = productBase.Name,
                    Description = productBase.Description,
                    Price = productBase.Price
                };
                _productRepository.InsertProduct(product);
                scope.Complete();
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
        }

        [HttpPut("UpdateProduct")]
        public IActionResult Put([FromBody] Product product)
        {
            if (product != null)
            {
                using (var scope = new TransactionScope())
                {
                    _productRepository.UpdateProduct(product);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult Delete(int id)
        {
            _productRepository.DeleteProduct(id);
            return new OkResult();
        }
    }
}

