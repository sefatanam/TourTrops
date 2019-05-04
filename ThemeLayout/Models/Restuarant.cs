using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ThemeLayout.Models
{
    public class Restuarant
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Review { get; set; }
        public double Price { get; set; }
        public string Duration { get; set; }
        public string Location { get; set; }
        public string Facilities { get; set; }
        public string Catagory { get; set; }
        public byte[] Photo { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageData { get; set; }
    }
}