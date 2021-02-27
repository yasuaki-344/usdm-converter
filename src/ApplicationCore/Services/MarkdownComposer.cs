// Copyright (c) 2021 Yasuaki Miyoshi
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using System.Linq;
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
            foreach (var upperRequirement in data.Requirements)
            {
                markdown += $"\n## [{upperRequirement.ID}]{upperRequirement.Summary}\n";
                markdown += $"\n### 理由\n\n{upperRequirement.Reason}\n";
                markdown += $"\n### 説明\n\n{upperRequirement.Description}\n";
                foreach (var lowerRequirement in upperRequirement.Requirements)
                {
                    markdown += $"\n### [{lowerRequirement.ID}]{lowerRequirement.Summary}\n";
                    markdown += $"\n#### 理由\n\n{lowerRequirement.Reason}\n";
                    markdown += $"\n#### 説明\n\n{lowerRequirement.Description}\n";

                    markdown += "\n#### 仕様\n";
                    foreach (var group in lowerRequirement.SpecificationGroups)
                    {
                        markdown += $"\n<{group.Category}>\n";
                        if (group.Specifications.Any())
                        {
                            markdown += "\n";
                        }
                        foreach (var specification in group.Specifications)
                        {
                            var check = specification.IsImplemented ? "x" : " ";
                            markdown += $"* [{check}] [{specification.ID}]{specification.Description}\n";
                        }
                    }
                }
            }
            return markdown;
        }
    }
}
