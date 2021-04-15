using System;
using System.ComponentModel.DataAnnotations;

namespace AppGmz.Models.DtoModels
{
    public class CreateRecordNewsDto
    {   
        [MaxLength(100)]
        [Required]
        public string Header { get; set; }
        [MaxLength(120)]
        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public string Body { get; set; }
        public string ImageUrl { get; set; }

        public DateTime DateTime { get; set; } = DateTime.UtcNow;

    }
}