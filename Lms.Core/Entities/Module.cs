using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Entities
{
    public class Module
    {
      
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
