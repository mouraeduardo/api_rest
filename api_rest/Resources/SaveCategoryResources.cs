using System.ComponentModel.DataAnnotations;

namespace api_rest.Resources
{
    public class SaveCategoryResources
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
