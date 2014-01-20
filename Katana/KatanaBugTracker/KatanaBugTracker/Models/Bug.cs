using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KatanaBugTracker.Models
{
    public class Bug
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public BugState State { get; set; }
    }
}