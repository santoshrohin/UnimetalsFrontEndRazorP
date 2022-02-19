using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models
{
    public class SelectListItem:CommonResponse
    {
        
        public List<Itemlist> result { get; set; }
    }
    public class Itemlist
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }
}
