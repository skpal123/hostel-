using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessHishab.Models
{
    public class CategoryInfo
    {
        public List< ItemInfo> ItemList { get; set; }
        public string CategoryName { get; set; }
    }
}