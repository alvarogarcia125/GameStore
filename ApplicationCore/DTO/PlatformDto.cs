﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTO
{
    public class PlatformDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Type {  get; set; }

    }

    public class PlatformRequestDto
    {

        [Required]
        public string Type { get; set; }
    }

}
