using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessHishab.Models
{
    public class BajarList
    {
        public System.Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public string Date { get; set; }
        public Guid PurchaserId { get; set; }
    }
}