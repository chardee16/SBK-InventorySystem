using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Services
{
    public class APIKey
    {
        public string key { get; set; } = "ad61ffdaab8298dcd03578e044322a48";
        public string accept { get; set; } = "*/*";
        public string token { get; set; } = "";
        public string http { get; set; } = "https://inventory-api-railway-production.up.railway.app";
    }
}
