using System;
using System.ComponentModel.DataAnnotations;

namespace AppGmz.Models.DtoModels
{
    public class FullRecordNewsDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Header { get; set; }
        [MaxLength(120)]
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTime DateTime { get; set; }
    }
}