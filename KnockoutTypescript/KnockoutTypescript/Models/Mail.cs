using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnockoutTypescript.Models
{
    /// <summary>
    /// Mail (used in 004 webmail)
    /// </summary>
    public class Mail
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public int FolderId { get; set; }
        public string MessageContent { get; set; }
    }
}