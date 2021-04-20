using System;
using System.ComponentModel.DataAnnotations;

namespace AppGmz.Models.DtoModels.VacanciesDTO
{
    public class ShowVacanciesDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DatePublication { get; set; }
    }
}