using System;
using System.ComponentModel.DataAnnotations;

namespace AppGmz.Models.DtoModels
{
    public class FindRecordNewsDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}