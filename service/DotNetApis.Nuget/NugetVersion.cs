﻿using System;
using Nito.Comparers;
using NuGet.Versioning;

namespace DotNetApis.Nuget
{
    /// <summary>
    /// Represents the version of a Nuget package. This is an immutable type that is comparable with itself (including comparison operators).
    /// </summary>
    public sealed class NugetVersion : ComparableBaseWithOperators<NugetVersion>
    {
        private readonly NuGetVersion _version;

        static NugetVersion()
        {
            DefaultComparer = ComparerBuilder.For<NugetVersion>().OrderBy(x => x._version, new VersionComparer());
        }

        internal NugetVersion(NuGetVersion version)
        {
            _version = version ?? throw new InvalidOperationException("version cannot be null");
        }

        /// <summary>
        /// Gets the major version number.
        /// </summary>
        public int Major => _version.Major;

        /// <summary>
        /// Gets the minor version number.
        /// </summary>
        public int Minor => _version.Minor;

        /// <summary>
        /// Gets the build (third) version number.
        /// </summary>
        public int Build => _version.Patch;

        /// <summary>
        /// Gets the revision (fourth) version number. This version number is not generally used.
        /// </summary>
        public int Revision => _version.Revision;

        /// <summary>
        /// Gets the prerelease string.
        /// </summary>
        public string Prerelease => _version.Release;

        /// <summary>
        /// Whether this package is a prerelease version.
        /// </summary>
        public bool IsPrerelease => Prerelease != string.Empty || Major == 0;

        /// <summary>
        /// Whether this package is a release version (i.e., not prerelease).
        /// </summary>
        public bool IsReleaseVersion => !IsPrerelease;

        /// <summary>
        /// Formats the Nuget version. The major and minor version numbers are always included; other version numbers are only included if non-zero, and the prerelease string is only present if non-empty.
        /// </summary>
        public override string ToString() => _version.ToNormalizedString();

        /// <summary>
        /// Attempts to parse a Nuget version. Returns <c>null</c> if the version number could not be parsed.
        /// </summary>
        /// <param name="version">The version, as a string.</param>
        public static NugetVersion TryParse(string version) => NuGetVersion.TryParse(version, out NuGetVersion result) ? new NugetVersion(result) : null;

        internal NuGetVersion ToNuGetVersion() => _version;

        internal static NugetVersion FromNuGetVersion(NuGetVersion version) => new NugetVersion(version);
    }
}
