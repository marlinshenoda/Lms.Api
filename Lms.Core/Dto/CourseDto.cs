﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Dto
{
    public class CourseDto
    {     
      [Required]
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate => StartDate.AddMonths(1);
        public  ICollection<ModuleDto>Modules { get; set; }

    }
}
