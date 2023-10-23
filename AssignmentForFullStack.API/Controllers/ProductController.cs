using AssignmentForFullStack.Core.Interfaces;
using AssignmentForFullStack.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentForFullStack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IBaseRepository<Product> productRepository;

        public ProductController(IBaseRepository<Product> _productRepository)
        {
            productRepository = _productRepository;
        }

        [HttpGet("{id:alpha}")]
        public IActionResult GetByCode(string id)
        {
            Product product = productRepository.GetByCode(id);
            if (product != null)
            {
                return Ok(product);
            }
            return BadRequest("Not found");
        }

        [HttpGet("GetAllProducts")]
        public IActionResult GetAllCategories()
        {
            return Ok(productRepository.GetAll(new[] { "Category" }));
        }
        [HttpGet("FilterByCat/{id:int}")]
        public IActionResult GetAllCategories(int id)
        {
            return Ok(productRepository.FindAll(p=>p.CatId==id));
        }

        [HttpPost]
        public IActionResult Create(Product prd)
        {
            Product product = productRepository.GetByCode(prd.ProductCode);
            if (product.Name != prd.Name)
            {
                return Ok(productRepository.Add(prd));
            }
            return BadRequest("Product already exits!");
        }

        [HttpPut]
        public IActionResult Update(Product prd)
        {
            Product product = productRepository.GetByCode(prd.ProductCode);
            if (product != null)
            {
                return Ok(productRepository.Update(prd));
            }
            return BadRequest("Product Not Found");
        }

        [HttpDelete]
        public IActionResult Delete(Product prd)
        {
            Product product = productRepository.GetByCode(prd.ProductCode);
            if (product != null)
            {
                return Ok(productRepository.Delete(prd));
            }
            return BadRequest("Product Not Found");
        }
    }
}
