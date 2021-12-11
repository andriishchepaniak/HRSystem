using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.DAL.Models
{
    public class Education
    {
        public int Id { get; set; }
        public string University { get; set; }
        public string Faculty { get; set; }
        public string Specialty { get; set; }
        public DateTime GraduationYear { get; set; }
    }
}
