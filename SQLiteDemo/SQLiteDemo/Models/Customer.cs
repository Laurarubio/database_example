using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDemo.Models
{
    class Customer
    {
        [SQLite.PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Contact { get; set; }
    }
}
