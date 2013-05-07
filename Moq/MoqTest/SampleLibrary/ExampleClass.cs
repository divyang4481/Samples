using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleLibrary
{
    public class ExampleClass
    {
        private ILongRunningLibrary _longRunningLibrary;

        public ExampleClass(ILongRunningLibrary longRunningLibrary)
        {
            _longRunningLibrary = longRunningLibrary;
        }

        // This is just example method using service
        public string GetData()
        {
            _longRunningLibrary.RunForALongTime(400);

            return "OK";
        }
    }
}
