using System;
using System.ComponentModel.DataAnnotations;

namespace AppGmz.Models.DtoModels
{
    public class RemoveRecordNewsDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}