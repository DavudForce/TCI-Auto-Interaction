using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adsl_Auto_Interaction_App
{
    internal class Status
    {
        /// <summary>
        /// Indicates if the operation was successful
        /// </summary>
        public bool? Success { get; set; }

        /// <summary>
        /// An integer number to indicate severity level of operation.
        /// </summary>
        public int Severity { get; set; } = 0;

        /// <summary>
        /// Main tag (with optional sub-tag)
        /// </summary>
        public TagInfo Tag { get; set; } = new TagInfo();

        /// <summary>
        /// Extra details if needed
        /// </summary>
        public string? Details { get; set; }
    }

    internal class TagInfo
    {
        /// <summary>
        /// Main tag name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Sub tag for extra classification
        /// </summary>
        public string? SubTag { get; set; }

        // Allow TagInfo to be used like a string
        public static implicit operator string?(TagInfo? tagInfo) => tagInfo?.Name;

        // Allow assigning a string directly to TagInfo
        public static implicit operator TagInfo?(string? tagName) => new TagInfo { Name = tagName };

        // For debugging / logging (Kind of pretty print extention)
        public override string ToString() => SubTag is null ? Name ?? "" : $"{Name}_{SubTag}";
    }
}