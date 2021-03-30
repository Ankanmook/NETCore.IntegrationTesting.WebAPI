using System.Linq;

namespace NETCore.IntegrationTesting.WebAPI.Data
{
    public class ValidationResult
    {
        public bool IsValid => !Errors.Any();
        public ErrorsList Errors { get; set; } = new ErrorsList();
    }
}