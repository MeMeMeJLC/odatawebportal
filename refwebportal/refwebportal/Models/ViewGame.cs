using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refwebportal.Models
{
    public class ViewGame
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? GameDateTime { get; set; }
    }
}