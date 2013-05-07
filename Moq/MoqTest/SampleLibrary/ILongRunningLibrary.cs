using System;

namespace SampleLibrary
{
    public interface ILongRunningLibrary
    {
        string RunForALongTime(int interval);
    }
}
