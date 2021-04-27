using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerEngine
{
    /// <summary>Interface ISchedulerEngine</summary>
    public interface ISchedulerEngine
    {
        Data.Models.Crew GetSuggestedAvailablePilot(List<Data.Models.Crew> crewList, List<Data.Models.Schedule> schedule, string location, DateTime tripStart, DateTime tripEnd);
    }
}
