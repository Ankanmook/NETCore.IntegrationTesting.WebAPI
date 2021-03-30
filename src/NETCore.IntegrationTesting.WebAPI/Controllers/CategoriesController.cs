using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.IntegrationTesting.WebAPI.Data;
using NETCore.IntegrationTesting.WebAPI.Models;

namespace NETCore.IntegrationTesting.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryProvider _categoryProvider;

        public CategoriesController(ICategoryProvider categoryProvider) =>
            _categoryProvider = categoryProvider;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseCache(Duration = 300)]
        public ActionResult<CategoriesOutputModel> GetAll()
        {
            var allowedCategories = _categoryProvider.AllowedCategories();

            return new CategoriesOutputModel(allowedCategories);
        }
    }
}