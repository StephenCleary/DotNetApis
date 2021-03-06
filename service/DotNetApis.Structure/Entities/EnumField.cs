﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetApis.Structure.Xmldoc;
using Newtonsoft.Json;

namespace DotNetApis.Structure.Entities
{
    /// <summary>
    /// Structured documentation for an enumeration value (static field).
    /// </summary>
    public sealed class EnumField
    {
        /// <summary>
        /// The name of this enumeration value.
        /// </summary>
        [JsonProperty("n")]
        public string Name { get; set; }

        /// <summary>
        /// The integral value of this enumeration value.
        /// </summary>
        [JsonProperty("v")]
        public object Value { get; set; }

        /// <summary>
        /// XML documentation.
        /// </summary>
        [JsonProperty("x")]
        public Xmldoc.Xmldoc Xmldoc { get; set; }

        public override string ToString() => Name + "=" + Value;
    }
}
