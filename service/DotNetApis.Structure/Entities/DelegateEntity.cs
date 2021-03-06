﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetApis.Structure.TypeReferences;
using DotNetApis.Structure.Xmldoc;
using Newtonsoft.Json;

namespace DotNetApis.Structure.Entities
{
    /// <summary>
    /// Structured documentation for a delegate.
    /// </summary>
    public sealed class DelegateEntity : IEntity, IHaveNamespace, IHaveGenericParameters
    {
        public EntityKind Kind => EntityKind.Delegate;
        public string DnaId { get; set; }
        public string Name { get; set; }
        public IReadOnlyList<AttributeJson> Attributes { get; set; }
        public EntityAccessibility Accessibility { get; set; }
        EntityModifiers IEntity.Modifiers { get; set; } // not used by delegates
        public Xmldoc.Xmldoc Xmldoc { get; set; }
        public string Namespace { get; set; }
        public IReadOnlyList<GenericParameterJson> GenericParameters { get; set; }

        /// <summary>
        /// Return type of the delegate.
        /// </summary>
        [JsonProperty("r")]
        public ITypeReference ReturnType { get; set; }

        /// <summary>
        /// The delegate parameters.
        /// </summary>
        [JsonProperty("p")]
        public IReadOnlyList<MethodParameter> Parameters { get; set; }

        public override string ToString()
        {
            var result = ReturnType + " " + Name;
            if (GenericParameters.Count != 0)
                result += "<" + string.Join(",", GenericParameters) + ">";
            result += "(" + string.Join(",", Parameters) + ")";
            return result;
        }
    }
}
