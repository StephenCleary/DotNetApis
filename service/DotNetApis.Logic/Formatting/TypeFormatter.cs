﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DotNetApis.Cecil;
using DotNetApis.Structure;
using DotNetApis.Structure.Entities;
using DotNetApis.Structure.TypeReferences;
using Mono.Cecil;

namespace DotNetApis.Logic.Formatting
{
    public sealed class TypeFormatter
    {
        private readonly AttributeFormatter _attributeFormatter;
        private readonly AccessibilityFormatter _accessibilityFormatter;
        private readonly ModifiersFormatter _modifiersFormatter;
        private readonly TypeReferenceFormatter _typeReferenceFormatter;
        private readonly NameFormatter _nameFormatter;
        private readonly GenericsFormatter _genericsFormatter;
        private readonly XmldocFormatter _xmldocFormatter;

        public TypeFormatter(AttributeFormatter attributeFormatter, AccessibilityFormatter accessibilityFormatter, ModifiersFormatter modifiersFormatter,
            TypeReferenceFormatter typeReferenceFormatter, NameFormatter nameFormatter, GenericsFormatter genericsFormatter, XmldocFormatter xmldocFormatter)
        {
            _attributeFormatter = attributeFormatter;
            _accessibilityFormatter = accessibilityFormatter;
            _modifiersFormatter = modifiersFormatter;
            _typeReferenceFormatter = typeReferenceFormatter;
            _nameFormatter = nameFormatter;
            _genericsFormatter = genericsFormatter;
            _xmldocFormatter = xmldocFormatter;
        }

        /// <summary>
        /// Formats a type declaration (classes, structs, and interfaces).
        /// </summary>
        /// <param name="type">The type to format.</param>
        /// <param name="formatMemberDefinition">The formatter for members.</param>
        public void Type(TypeDefinition type, Action<IMemberDefinition, StreamingJsonWriter> formatMemberDefinition, StreamingJsonWriter doc)
        {
            EntityKind kind;
            IReadOnlyList<AttributeJson> attributes = _attributeFormatter.Attributes(type).ToList();
            var accessibility = _accessibilityFormatter.TypeDefinitionAccessibility(type);
            var modifiers = EntityModifiers.None;
            IReadOnlyList<ITypeReference> baseTypesAndInterfaces = null;
            string ns = null;

            if (type.IsInterface)
                kind = EntityKind.Interface;
            else if (type.IsValueType)
                kind = EntityKind.Struct;
            else
            {
                modifiers = _modifiersFormatter.TypeDefinitionModifiers(type);
                kind = EntityKind.Class;
            }

            var allGenericDeclaringTypesAndThis = type.GenericDeclaringTypesAndThis().ToArray();
            var typeWithGenericParameters = allGenericDeclaringTypesAndThis[allGenericDeclaringTypesAndThis.Length - 1];
            if (allGenericDeclaringTypesAndThis.Length == 1)
                ns = allGenericDeclaringTypesAndThis[0].Reference.Namespace;

            if (type.BaseTypeAndInterfaces().Any())
                baseTypesAndInterfaces = type.BaseTypeAndInterfaces().Select(_typeReferenceFormatter.TypeReference).ToList();

            var members = type.ExposedMembers().ToList();
            doc.WriteStartObject();
            doc.WriteProperty("i", type.DnaId());
            doc.WriteProperty("k", kind);
            doc.WriteProperty("b", attributes);
            doc.WriteProperty("a", accessibility);
            doc.WriteProperty("n", _nameFormatter.EscapeIdentifier(typeWithGenericParameters.Name));
            doc.WriteProperty("m", modifiers);
            doc.WriteProperty("s", ns);
            doc.WriteProperty("g", _genericsFormatter.GenericParameters(type, typeWithGenericParameters.GenericParameters).ToList());
            doc.WriteProperty("t", baseTypesAndInterfaces);
            doc.WriteProperty("x", _xmldocFormatter.Xmldoc(type));
            doc.WritePropertyName("e");
            doc.WriteStartObject();
            doc.WritePropertyName("l");
            doc.WriteStartArray();
            foreach (var member in members.Where(x => SemanticOrdering.PrimaryMemberGrouping(x) / 10 == 0).OrderBy(x => x, SemanticOrdering.MemberComparer))
                formatMemberDefinition(member, doc);
            doc.WriteEndArray();
            doc.WritePropertyName("s");
            doc.WriteStartArray();
            foreach (var member in members.Where(x => SemanticOrdering.PrimaryMemberGrouping(x) / 10 == 1).OrderBy(x => x, SemanticOrdering.MemberComparer))
                formatMemberDefinition(member, doc);
            doc.WriteEndArray();
            doc.WritePropertyName("i");
            doc.WriteStartArray();
            foreach (var member in members.Where(x => SemanticOrdering.PrimaryMemberGrouping(x) / 10 == 2).OrderBy(x => x, SemanticOrdering.MemberComparer))
                formatMemberDefinition(member, doc);
            doc.WriteEndArray();
            doc.WritePropertyName("t");
            doc.WriteStartArray();
            foreach (var member in members.Where(x => SemanticOrdering.PrimaryMemberGrouping(x) / 10 == 10).OrderBy(x => x, SemanticOrdering.MemberComparer))
                formatMemberDefinition(member, doc);
            doc.WriteEndArray();
            doc.WriteEndObject();
            doc.WriteEndObject();
        }
    }
}
