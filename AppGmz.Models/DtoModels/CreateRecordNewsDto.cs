using System;
using System.ComponentModel.DataAnnotations;

namespace AppGmz.Models.DtoModels
{
    public class CreateRecordNewsDto
    {
        [Required]
        public string Header { get; set; }
        [Required]
        public string SubTitles { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTime DateTime { get; set; } = DateTime.UtcNow;
    }
}