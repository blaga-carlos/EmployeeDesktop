using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    internal class Department
    {
        public int departmentId { get; set; }
        public string description { get; set; }
        public int parentID { get; set; }
        public int managerId { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
