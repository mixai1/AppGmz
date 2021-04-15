using System.ComponentModel.DataAnnotations;

namespace AppGmz.Models.DomainModels
{
    public class Persons : EntityBase
    {
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
        [Required]
        public string Position { get; set; }

    }
}