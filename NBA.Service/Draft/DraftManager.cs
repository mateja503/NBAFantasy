using System;
using System.Collections.Generic;
using System.Text;

namespace NBA.Service.Draft
{
    public class DraftManager
    {
        public DraftState CurrentState { get; private set; } = new DraftState
        {
            LeagueName = "NBA Fantasy Elite",
            IsPaused = true,
            PickEndTime = DateTime.UtcNow.AddSeconds(60)//for now 60 configuratble latter
        };

        public void StartNewPick(int seconds = 60) 
        {
            CurrentState.PickEndTime = DateTime.UtcNow.AddSeconds(seconds);
            CurrentState.IsPaused = false;
        }
        void PauseDraft() => CurrentState.IsPaused = true;
    }
}
