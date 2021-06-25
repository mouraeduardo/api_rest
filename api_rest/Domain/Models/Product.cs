using api_rest.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api_rest.Domain.Models
{
    //[Table("products")]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short QuantityInPackge { get; set; }
        public EUnitOfMeasurement UnitOfMeasurement { get; set; }

        public int IdCategory { get; set; }
        public Category Category;

        
    }
}
