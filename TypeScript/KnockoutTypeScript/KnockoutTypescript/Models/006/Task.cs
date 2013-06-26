using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnockoutTypescript.Models._006
{
    [Serializable]
    public class Task
    {
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public bool _destroy { get; set; }
    }
}