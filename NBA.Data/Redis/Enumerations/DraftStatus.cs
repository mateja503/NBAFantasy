using System;
using System.Collections.Generic;
using System.Text;

namespace NBA.Data.Redis.Enumerations
{
    public enum DraftStatus
    {
        Initial = 0,
        DraftStarted = 1,
        Paused = 2,
        DraftEnded = 3,
        DraftCompleted = 4
    }
}
