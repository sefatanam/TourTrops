using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ThemeLayout.Models
{
    public class Search
    {
        public long Id { get; set; }
        public double Ammount { get; set; }
        public string Details { get; set; }
       


    }
}