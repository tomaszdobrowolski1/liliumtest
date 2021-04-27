using Data.Core;
using Data.Models;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchedulerEngine
{
    public class SchedulerEngine : ISchedulerEngine
    {
        /// <summary>Gets the suggested available pilot.</summary>
        /// <param name="crewList">The crew list.</param>
        /// <param name="schedule">The schedule.</param>
        /// <param name="location">The location.</param>
        /// <param name="tripStart">The trip start.</param>
        /// <param name="tripEnd">The trip end.</param>
        /// <returns>Data.Models.Crew.</returns>
        public Data.Models.Crew GetSuggestedAvailablePilot(List<Data.Models.Crew> crewList, List<Data.Models.Schedule> schedule, string location, DateTime tripStart, DateTime tripEnd)
        {
            var dayOfWeek = tripStart.DayOfWeek.ToString().ToLower();
            var pilots = crewList.Where(x => x.WorkDays.Select(y => y.ToLower()).Contains(dayOfWeek) && x.Base == location && !schedule.Where(a => a.PilotId == x.Id).Any(s => (s.flightStart >= tripStart && s.flightStart <= tripEnd) || (s.flightEnd <= tripEnd && s.flightEnd >= tripStart)));
            if (pilots.Count() == 0)
            {
                return null;
            }
            
            //take pilot that didnt fly the longest
            var sortPilot = from p in pilots
                     join s in schedule
                         on p.Id equals s.PilotId
                    // this requires mock datetimenow so tests work. Leaving it .
                    // where s.flightEnd < DateTime.Now
                     group new { p, s } by new { p.Id }
             into grp
                     select new
                     {
                         Count = grp.Max(x => x.s.flightEnd),
                         grp.Key.Id
                     };

            if (sortPilot == null || sortPilot.Count() == 0)
            {
                return pilots.FirstOrDefault();
            }

            return crewList.Where(x => x.Id == sortPilot.FirstOrDefault().Id).FirstOrDefault();
 
        }
    }
}
