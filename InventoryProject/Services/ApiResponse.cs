using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Services
{
    public class ApiResponse<T>
    {
        public string status { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
}
