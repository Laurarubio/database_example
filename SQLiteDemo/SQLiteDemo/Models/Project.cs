using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDemo.Models
{
    class Project
    {
        [SQLite.PrimaryKey]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
