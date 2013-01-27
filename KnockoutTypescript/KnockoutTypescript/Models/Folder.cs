using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnockoutTypescript.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Mail> Mails { get; set; }
    }
}