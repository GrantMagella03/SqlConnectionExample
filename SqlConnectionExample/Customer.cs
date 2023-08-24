using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConnectionExample {
    public class Customer {
        public int ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public decimal? Sales { get; set; }
        public bool Active { get; set; }
    }
}
