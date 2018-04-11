using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessHishab.Models
{
    public class MealList
    {
        public System.Guid Id { get; set; }
        public System.Guid MemberId { get; set; }
        public double Lunch { get; set; }
        public double Morning { get; set; }
        public double Dinner { get; set; }
        public string Date { get; set; }
    }
}