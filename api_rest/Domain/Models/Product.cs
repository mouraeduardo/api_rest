using api_rest.Domain.Helpers;

namespace api_rest.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short QuantityInPackge { get; set; }
        public EUnitOfMeasurement UnitOfMeasurement { get; set; }
        public int IdCategory { get; set; }
        public Category Category { get; set; }

    }
}
