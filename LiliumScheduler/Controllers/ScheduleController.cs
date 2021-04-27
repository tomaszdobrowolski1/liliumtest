using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using SchedulerEngine;

namespace LiliumScheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {

        /// <summary>Gets the suggested available pilot.</summary>
        /// <param name="pilotId">The pilot identifier.</param>
        /// <param name="location">The location.</param>
        /// <param name="tripStart">The trip start.</param>
        /// <param name="returnDate">The return date.</param>
        /// <returns>Crew.</returns>
        [HttpPost]
        public Crew GetSuggestedAvailablePilot([FromBody] int pilotId, string location,DateTime tripStart,DateTime returnDate)
        {
            IJsonMapper map = new JsonMapper();
            var crew = map.GetCollectionFromJson<Crew>();
            var schedule = map.GetCollectionFromJson<Schedule>();
            ISchedulerEngine e = new SchedulerEngine.SchedulerEngine();
            //assuming trip takes 2 hours
            return e.GetSuggestedAvailablePilot(crew, schedule,location,tripStart, returnDate.AddHours(2));
        }

        /// <summary>Schedules the flight for pilot.</summary>
        /// <param name="pilotId">The pilot identifier.</param>
        /// <param name="location">The location.</param>
        /// <param name="tripStart">The trip start.</param>
        /// <param name="returnDate">The return date.</param>
        /// <returns>
        ///   <c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [HttpPost]
        public bool ScheduleFlightForPilot([FromBody] int pilotId, string location, DateTime tripStart, DateTime returnDate)
        {
            try
            {
                IJsonMapper map = new JsonMapper();
                var crew = map.GetCollectionFromJson<Crew>();
                var schedule = map.GetCollectionFromJson<Schedule>();
                // check if possible, obviously..

                Schedule s = new Schedule();
                //assuming 2 hour flight
                s.flightStart = tripStart;
                s.flightEnd = tripStart.AddHours(2);
                s.PilotId = pilotId;
                s.Source = location;
                //destination??

                s.FlightStatus = FlightStatus.NotStarted;

                Schedule s2 = new Schedule();
                //assuming 2 hour flight
                s2.flightStart = tripStart;
                s2.flightEnd = tripStart.AddHours(2);
                s2.PilotId = pilotId;
                s2.Source = location;
                //destination??

                s2.FlightStatus = FlightStatus.NotStarted;

                schedule.Add(s);
                schedule.Add(s2);
                map.SaveCollectionToJson(schedule);

                ISchedulerEngine e = new SchedulerEngine.SchedulerEngine();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

   

    }
}
