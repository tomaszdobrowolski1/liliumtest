using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Schedule : Model
    {
        /// <summary>Gets or sets the pilot identifier.</summary>
        /// <value>The pilot identifier.</value>
        public int PilotId { get; set; }

        /// <summary>
        ///   <para>
        /// Gets or sets the flight start.
        /// </para>
        /// </summary>
        /// <value>The flight start.</value>
        public DateTime flightStart { get;set;}

        /// <summary>Gets or sets the flight end.</summary>
        /// <value>The flight end.</value>
        public DateTime flightEnd { get; set; }

        /// <summary>Gets or sets the source.</summary>
        /// <value>The source.</value>
        public string Source { get; set; }

        /// <summary>Gets or sets the destination.</summary>
        /// <value>The destination.</value>
        public string Destination { get; set; }

        /// <summary>Gets or sets the flight status.</summary>
        /// <value>The flight status.</value>
        public FlightStatus FlightStatus { get; set; }
    }
}
