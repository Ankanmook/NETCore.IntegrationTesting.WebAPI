using Microsoft.AspNetCore.Mvc.ModelBinding;
using NETCore.IntegrationTesting.WebAPI.Data;

namespace NETCore.IntegrationTesting.WebAPI.Core
{
    public static class ModelStateExtensions
    {
        public static ModelStateDictionary AddValidationResultErrors(this ModelStateDictionary modelState, ErrorsList errors)
        {
            foreach (var (key, value) in errors)
            {
                modelState.AddModelError(key, value);
            }

            return modelState;
        }
    }
}