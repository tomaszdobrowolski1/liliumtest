using Data.Core;
using Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchedulerEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestClass]
    public class SchedulerEngineTest
    {
        [TestMethod]
        public void pilotAvailableWithExactGap()
        {
            var crew = new List<Crew>();
            var c = new Crew();
            c.Id = 1;
            c.Name = "tomasz";
            c.WorkDays = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            c.Base = "Munich";
            crew.Add(c);
            var schedule = new List<Schedule>();
            var s = new Schedule();
            s.flightStart = new DateTime(2020, 12, 20, 10, 30, 00);
            s.flightEnd = new DateTime(2020, 12, 20, 12, 29, 00);
            s.PilotId = 1;
            var s2 = new Schedule();
            s2.flightStart = new DateTime(2020, 12, 20, 16, 31, 00);
            s2.flightEnd = new DateTime(2020, 12, 20, 18, 31, 00);
            s2.PilotId = 1;
            schedule.Add(s);
            schedule.Add(s2);

            ISchedulerEngine scheduler = new SchedulerEngine.SchedulerEngine();
            var pilot= scheduler.GetSuggestedAvailablePilot(crew, schedule, "Munich", new DateTime(2020, 12, 20, 12, 30, 00), new DateTime(2020, 12, 20, 16, 30, 00));
            Assert.IsTrue(pilot != null);
        }
        [TestMethod]
        public void pilotNotAvailableStartDatesOverlapping()
        {
            var crew = new List<Crew>();
            var c = new Crew();
            c.Id = 1;
            c.Name = "tomasz";
            c.WorkDays = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            c.Base = "Munich";
            crew.Add(c);
            var schedule = new List<Schedule>();
            var s = new Schedule();
            s.flightStart = new DateTime(2020, 12, 20, 10, 30, 00);
            s.flightEnd = new DateTime(2020, 12, 20, 12, 29, 00);
            s.PilotId = 1;
            var s2 = new Schedule();
            s2.flightStart = new DateTime(2020, 12, 20, 16, 29, 00);
            s2.flightEnd = new DateTime(2020, 12, 20, 18, 31, 00);
            s2.PilotId = 1;
            schedule.Add(s);
            schedule.Add(s2);

            ISchedulerEngine scheduler = new SchedulerEngine.SchedulerEngine();
            var pilot = scheduler.GetSuggestedAvailablePilot(crew, schedule, "Munich", new DateTime(2020, 12, 20, 12, 30, 00), new DateTime(2020, 12, 20, 16, 30, 00));
            Assert.IsTrue(pilot == null);
        }
        [TestMethod]
        public void pilotNotAvailableEndDatesOverlapping()
        {
            var crew = new List<Crew>();
            var c = new Crew();
            c.Id = 1;
            c.Name = "tomasz";
            c.WorkDays = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            c.Base = "Munich";
            crew.Add(c);
            var schedule = new List<Schedule>();
            var s = new Schedule();
            s.flightStart = new DateTime(2020, 12, 20, 10, 30, 00);
            s.flightEnd = new DateTime(2020, 12, 20, 12, 31, 00);
            s.PilotId = 1;
            var s2 = new Schedule();
            s2.flightStart = new DateTime(2020, 12, 20, 16, 31, 00);
            s2.flightEnd = new DateTime(2020, 12, 20, 18, 30, 00);
            s2.PilotId = 1;
            schedule.Add(s);
            schedule.Add(s2);

            ISchedulerEngine scheduler = new SchedulerEngine.SchedulerEngine();
            var pilot = scheduler.GetSuggestedAvailablePilot(crew, schedule, "Munich", new DateTime(2020, 12, 20, 12, 30, 00), new DateTime(2020, 12, 20, 16, 30, 00));
            Assert.IsTrue(pilot == null);
        }
        [TestMethod]
        public void NoSchedule_pilotIsSelected()
        {
            var crew = new List<Crew>();
            var c = new Crew();
            c.Id = 1;
            c.Name = "tomasz";
            c.WorkDays = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            c.Base = "Munich";
            crew.Add(c);
            var schedule = new List<Schedule>();

            ISchedulerEngine scheduler = new SchedulerEngine.SchedulerEngine();
            var pilot = scheduler.GetSuggestedAvailablePilot(crew, schedule, "Munich", new DateTime(2020, 12, 20, 12, 30, 00), new DateTime(2020, 12, 20, 16, 30, 00));
            Assert.IsTrue(pilot != null);
        }
        [TestMethod]
        public void pilotThatWaitsLongestIsSelected()
        {
            var crew = new List<Crew>();
            var c = new Crew();
            c.Id = 1;
            c.Name = "tomasz";
            c.WorkDays = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            c.Base = "Munich";
            crew.Add(c);
            var c2 = new Crew();
            c2.Id = 2;
            c2.Name = "Jack";
            c2.WorkDays = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            c2.Base = "Munich";
            crew.Add(c2);
            var schedule = new List<Schedule>();
            var s = new Schedule();
            s.flightStart = new DateTime(2019, 12, 18, 10, 30, 00);
            s.flightEnd = new DateTime(2019, 12, 18, 12, 31, 00);
            s.PilotId = 1;
            schedule.Add(s);


            var s2 = new Schedule();
            s2.flightStart = new DateTime(2019, 12, 19, 10, 30, 00);
            s2.flightEnd = new DateTime(2019, 12, 19, 12, 31, 00);
            s2.PilotId = 2;
            schedule.Add(s2);

            var s3 = new Schedule();
            s3.flightStart = new DateTime(2019, 12, 16, 10, 30, 00);
            s3.flightEnd = new DateTime(2019, 12, 16, 12, 31, 00);
            s3.PilotId = 1;
            schedule.Add(s3);
            ISchedulerEngine scheduler = new SchedulerEngine.SchedulerEngine();
            var pilot = scheduler.GetSuggestedAvailablePilot(crew, schedule, "Munich", new DateTime(2020, 12, 20, 12, 30, 00), new DateTime(2020, 12, 20, 16, 30, 00));
            Assert.IsTrue(pilot.Id == 1);
        }
    }
}
