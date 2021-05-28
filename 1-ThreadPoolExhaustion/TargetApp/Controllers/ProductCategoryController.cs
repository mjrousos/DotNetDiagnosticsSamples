using System;
using System.Collections.Generic;
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
    }
}
