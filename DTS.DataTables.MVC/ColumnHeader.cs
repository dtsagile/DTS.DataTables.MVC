using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTS.DataTables.MVC
{
    public class ColumnHeader
    {
        public string data { get; set; }
        public string  name { get; set; }
        public bool visible { get; set; }
        public int target { get; set; }
    }
}
