using System;
using System.ComponentModel.DataAnnotations;

namespace AppGmz.Models.DtoModels.VacanciesDTO
{
    public class ShowVacanciesDto
    {
        [Required]
        [MaxLength(120)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DatePublication { get; set; }
    }
}