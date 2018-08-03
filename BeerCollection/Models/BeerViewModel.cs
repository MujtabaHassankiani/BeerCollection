using System.ComponentModel.DataAnnotations;

namespace BeerCollection.Models
{
    public class BeerData
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        public int Rating { get; set; }
    }
}