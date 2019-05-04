using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ThemeLayout.Models
{
    public class Transport
    {
        public long Id { get; set; }
        public string Name { get; set; } 
        public string From { get; set; }
        public string To { get; set; }
        public DateTime DateTime { get; set; }
        public string Facilities { get; set; }
        public string BusStation { get; set; }
        public double Price { get; set; }
        public byte[] Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageData { get; set; }
    }
}