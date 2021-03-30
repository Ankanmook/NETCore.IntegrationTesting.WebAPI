using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NETCore.IntegrationTesting.WebAPI.Data;
using NETCore.IntegrationTesting.WebAPI.Models;
using NETCore.IntegrationTesting.WebAPI.Stock;

namespace NETCore.IntegrationTesting.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IProductDataRepository _productDataRepository;
        private readonly IStockCalculator _stockCalculator;

        public StockController(IProductDataRepository productDataRepository, IStockCalculator stockCalculator)
        {
            _productDataRepository = productDataRepository;
            _stockCalculator = stockCalculator;
        }

        [HttpGet("total")]
        public async Task<ActionResult<StockTotalOutputModel>> GetStockTotal()
        {
            var products = await _productDataRepository.GetProductsAsync();

            var totalStockCount = _stockCalculator.CalculateStockTotal(products);

            return Ok(new StockTotalOutputModel { StockItemTotal = totalStockCount });
        }
    }
}