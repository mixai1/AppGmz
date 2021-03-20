﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AppGmz.Models.DtoModels
{
    public class FoundRecordNewsDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Header { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTime DateTime { get; set; }
    }
}