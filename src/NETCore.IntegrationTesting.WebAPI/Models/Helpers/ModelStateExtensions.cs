using Microsoft.AspNetCore.Mvc.ModelBinding;
using NETCore.IntegrationTesting.WebAPI.Data;

namespace NETCore.IntegrationTesting.WebAPI.Models.Helpers
{
    public static class ModelStateExtensions
    {
        public static ModelStateDictionary AddValidationResultErrors(this ModelStateDictionary modelState,
            ErrorsList errors)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError(error.Key, error.Value);
            }

            return modelState;
        }
    }
}