using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvalCore.Entities
{
    public class Client
    {
        [Key]
        public int IdClient { get; set; }
        public string Name { get; set; }
        public string RFC { get; set; }
        public string Address { get; set; }
        public string Ciudad { get; set; }
        public string ZipCode { get; set; }
    }
}
