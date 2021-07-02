using api_rest.Domain.Helpers;
using api_rest.Domain.Models;
using System;
using System.Collections.Generic;

namespace api_rest.Resources
{
    public class ProductResources
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EUnitOfMeasurement UnitOfMeasurement { get; set; }
        public int QuantityInPackge { get; set; }
        public Category Category { get; set; }
    }
}
