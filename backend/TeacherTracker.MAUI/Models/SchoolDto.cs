using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherTracker.MAUI.Models
{
    public class SchoolDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string OIB { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
