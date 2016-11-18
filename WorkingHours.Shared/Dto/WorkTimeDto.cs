﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Shared.Dto
{
    public class WorkTimeDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public double Hours { get; set; }

        public byte[] RowVersion { get; set; }

        public bool CanUpdate { get; set; }
    }
}
