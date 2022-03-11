using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Domain.Entities
{
    public class ESDocument
    {
        public string Name { get; set; }
        public string EmpId { get; set; }
        public List<string> Skills { get; set; }
    }
}
