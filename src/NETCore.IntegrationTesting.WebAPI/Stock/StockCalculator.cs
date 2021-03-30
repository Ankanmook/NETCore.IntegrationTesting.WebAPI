using System.Collections.Generic;
using System.Linq;
using NETCore.IntegrationTesting.WebAPI.Models.DomainModels;

namespace NETCore.IntegrationTesting.WebAPI.Stock
{
    public class StockCalculator : IStockCalculator
    {
        public int CalculateStockTotal(IEnumerable<Product> products) => products.Sum(p => p.StockCount);
    }
}