using AssignmentForFullStack.Core.Interfaces;
using AssignmentForFullStack.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentForFullStack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IBaseRepository<Category> categoryRepository;

        public CategoryController(IBaseRepository<Category> _categoryRepository)
        {
            categoryRepository = _categoryRepository;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Category category = categoryRepository.GetById(id);
            if(category !=null)
            {
                return Ok(category);
            }
            return BadRequest("Not found");
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Ok(categoryRepository.GetAll(new[] {"Products"}));
        }

        [HttpPost]
        public IActionResult Create(Category cat)
        {
            Category category = categoryRepository.GetById(cat.Id);
            if (cat.Name==category.Name)
            {
                return BadRequest("This Category already exists!");
            }
            return Ok(categoryRepository.Add(cat));
        }

        [HttpPut]
        public IActionResult Update(Category cat)
        {
            Category category = categoryRepository.GetById(cat.Id);
            if (category != null)
            {
                return Ok(categoryRepository.Update(cat));
            }
            return BadRequest("This Category Not found");
        }

        [HttpDelete]
        public IActionResult Delete(Category cat) 
        {
            Category category = categoryRepository.GetById(cat.Id);
            if (category != null)
            {
                return Ok(cat);
            }
            return BadRequest("This Category Not found");
        }
        
    }
}
