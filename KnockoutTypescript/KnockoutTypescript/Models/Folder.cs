using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnockoutTypescript.Models
{
    /// <summary>
    /// Mail folder (used in 004 webmail)
    /// </summary>
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Mail> Mails { get; set; }
    }
}