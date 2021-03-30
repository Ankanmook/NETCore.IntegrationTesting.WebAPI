using System.Collections.Generic;
using NETCore.IntegrationTesting.WebAPI.Models.DomainModels;

namespace NETCore.IntegrationTesting.WebAPI.Stock
{
    public interface IStockCalculator
    {
        int CalculateStockTotal(IEnumerable<Product> products);
    }
}