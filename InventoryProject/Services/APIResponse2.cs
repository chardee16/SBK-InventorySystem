using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Services
{
    public class ApiResponse2<T>
    {
        public string status { get; set; }
        public List<T> data { get; set; } = new List<T>();
    }
}
