using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace client
{
    internal class Employee
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        public string name { get; set; }

        public int departmentId { get; set; }

        public int managerId { get; set; }

        public string email { get; set; }

        public string password { get; set; }
    }

}
