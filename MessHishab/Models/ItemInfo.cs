using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessHishab.Models
{
    public class ItemInfo
    {
        public System.Guid Id { get; set; }
        public int SerialNo { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string UnitName { get; set; }

    }
}