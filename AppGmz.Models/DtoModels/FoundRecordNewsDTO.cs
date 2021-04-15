using System;
using System.ComponentModel.DataAnnotations;

namespace AppGmz.Models.DtoModels
{
    public class FoundRecordNewsDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Header { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }

        public DateTime DateTime { get; set; }
    }
}