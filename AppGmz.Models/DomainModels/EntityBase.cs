using System;
using System.ComponentModel.DataAnnotations;

namespace AppGmz.Models.DomainModels
{
    public abstract class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}