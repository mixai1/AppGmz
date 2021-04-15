using System;
using System.ComponentModel.DataAnnotations;

namespace AppGmz.Models.DomainModels
{
    public class RecordNews : EntityBase
    {
        [Required]
        public string Header { get; set; }
        [MaxLength(120)]
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string Body { get; set; }
        public string ImageURL { get; set; }
        public DateTime DateTime { get; set; }
    }
}