using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TargetApp.Models;

namespace TargetApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly CategoryRepository _repository;
        private readonly ILogger<ProductCategoryController> _logger;

        public ProductCategoryController(CategoryRepository repository, ILogger<ProductCategoryController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public IEnumerable<ProductCategory> Get() => _repository.GetAllCategories();

        [HttpGet("async")]
        public async Task<IEnumerable<ProductCategory>> GetAsync() => await _repository.GetAllCategoriesAsync();

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ProductCategory>> Get(int id)
        {
            var category = _repository.GetCategory(id);
            
            if (category is null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpGet("async/{id}")]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetAsync(int id)
        {
            var category = await _repository.GetCategoryAsync(id);

            if (category is null)
            {
                return NotFound();
            }

            return Ok(category);
        }
    }
}
