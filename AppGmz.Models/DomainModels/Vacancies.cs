using System;
using System.ComponentModel.DataAnnotations;

namespace AppGmz.Models.DomainModels
{
    public class Vacancies : EntityBase
    {
        [Required]
        [MaxLength(120)]
        public string Name { get; set; }
        public int Salary { get; set; }
        [Required]
        public string Description { get; set; }
        public string Requirements { get; set; }
        public string WorkExperience { get; set; }
        [Required]
        public DateTime DatePublication { get; set; }
    }
}