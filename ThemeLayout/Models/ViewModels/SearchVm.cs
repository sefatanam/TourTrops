using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThemeLayout.Models.ViewModels
{
    public class SearchVm
    {

        public string From { get; set; }
        public string To { get; set; } 
        public DateTime? Date { get; set; }
        public double BudgetFrom { get; set; }
        public double BudgetTo { get; set; }

        public List<Hotel> Hotels { get; set; }
        public List<Transport> Transports { get; set; }


    }
}