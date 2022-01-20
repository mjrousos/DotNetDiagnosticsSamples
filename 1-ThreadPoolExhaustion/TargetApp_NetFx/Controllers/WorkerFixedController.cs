using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TargetApp.Models;

namespace TargetApp.Controllers
{
    public class WorkerFixedController : ApiController
    {
        private readonly CategoryRepository _repository;

        public WorkerFixedController(CategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductCategory>> GetAsync() => await _repository.GetAllCategoriesAsync();

        [HttpGet]
        public async Task<IHttpActionResult> GetAsync(int id)
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
