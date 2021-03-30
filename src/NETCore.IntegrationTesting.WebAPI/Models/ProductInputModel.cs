using System;
using System.ComponentModel.DataAnnotations;
using NETCore.IntegrationTesting.WebAPI.Models.DomainModels;

namespace NETCore.IntegrationTesting.WebAPI.Models
{
    public class ProductInputModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required, StringLength(256)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required, Range(0.01, 10000)]
        public decimal Price { get; set; }

        public Product ToProduct()
        {
            var product = new Product(Id)
            {
                Name = Name,
                ShortDescription = Description,
                Category = Category,
                Price = Price,
            };

            return product;
        }
    }
}