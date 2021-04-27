using System;
using System.Collections.Generic;

namespace Data.Models
{
    /// <summary>Class Crew.
    /// Implements the <see cref="Data.Models.Model" /></summary>
    public class Crew : Model
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>Gets or sets the base.</summary>
        /// <value>The base.</value>
        public string Base { get; set; }

        /// <summary>Gets or sets the work days.</summary>
        /// <value>The work days.</value>
        public List<string> WorkDays { get; set; }
    }
}
