﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetApis.Structure.Util;
using Newtonsoft.Json;

namespace DotNetApis.Structure
{
    /// <summary>
    /// A primitive literal value.
    /// </summary>
    public sealed class PrimitiveLiteral : ILiteral
    {
        public EntityLiteralKind Kind => EntityLiteralKind.Primitive;

        /// <summary>
        /// The value; may be a string, boolean, char, byte, sbyte, short, ushort, int, uint, long, ulong, single, double, or decimal.
        /// </summary>
        [JsonProperty("v")]
        public object Value { get; set; }

        /// <summary>
        /// Whether the context of this literal value implies it should be displayed in hexadecimal rather than decimal (only applies to integral values).
        /// </summary>
        [JsonProperty("h"), JsonConverter(typeof(IntBooleanConverter))]
        public bool PreferHex { get; set; }
    }
}