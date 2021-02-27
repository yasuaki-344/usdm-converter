// Copyright (c) 2021 Yasuaki Miyoshi
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using UsdmConverter.ApplicationCore.Entities;
using UsdmConverter.ApplicationCore.Interfaces;

namespace UsdmConverter.ApplicationCore.Services
{
    public class MarkdownComposer : IMarkdownComposer
    {
        /// <summary>
        /// Initializes a new instance of MarkdownComposer class.
        /// </summary>
        public MarkdownComposer()
        {
        }

        public string Compose(RequirementSpecification data)
        {
            var markdown = string.Empty;
            markdown += $"# {data.Title}\n";

            return markdown;
        }
    }
}
