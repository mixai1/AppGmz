using System;
using System.ComponentModel.DataAnnotations;

namespace AppGmz.Models.DomainModels
{
    public class RecordNews : EntityBase
    {
        [Required]
        public string Header { get; set; }
        [Required]
        public string SubTitles { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTime DateTime { get; set; }
    }
}