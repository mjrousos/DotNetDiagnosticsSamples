using System.Collections.Generic;
using System.Web.Http;
using TargetApp.Models;

namespace TargetApp.Controllers
{
    public class WorkerController : ApiController
    {
        private readonly CategoryRepository _repository;

        public WorkerController(CategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<ProductCategory> Get() => _repository.GetAllCategories();

        [HttpGet]
        public IHttpActionResult Get(int id)
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
