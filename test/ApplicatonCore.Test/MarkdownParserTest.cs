using System.Linq;
using Microsoft.Toolkit.Parsers.Markdown;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;
using Microsoft.Toolkit.Parsers.Markdown.Inlines;
using UsdmConverter.ApplicationCore.Entities;
using UsdmConverter.ApplicationCore.Logic;
using UsdmConverter.ApplicationCore.Services;
using Xunit;
using Xunit.Sdk;

namespace UsdmConverter.ApplicatonCore.Test
{
    public class MarkdownParserTest
    {
        [Fact]
        public void ParseTitleCorrectly()
        {
            var target = new MarkdownParser();
            var actual = target.Parse("# title");

            Assert.Equal("title", actual.Title);
        }

        [Fact]
        public void ParseUpperRequirementsCorrectly()
        {
            var markdown = @"# title

## [REQ01]Requirement1

### 理由

reason1

### 説明

description1

### [REQ01-01]requirement1-1

#### 理由

reason1-1

#### 説明

description1-1

#### 仕様

<仕様グループ>

* [ ] [SPC01-01-01]specification1
* [x] [SPC01-01-02]specification2
";
            var target = new MarkdownParser();
            var actual = target.Parse(markdown);

            Assert.Equal(1, actual.Requirements.Count);
            var requirement = actual.Requirements[0];
            Assert.Equal("REQ01", requirement.ID);
            Assert.Equal("Requirement1", requirement.Summary);
            Assert.Equal("reason1", requirement.Reason);
            Assert.Equal("description1", requirement.Description);
            var lowerRequirement = requirement.Requirements[0];
            Assert.Equal("REQ01-01", lowerRequirement.ID);
            Assert.Equal("requirement1-1", lowerRequirement.Summary);
            Assert.Equal("reason1-1", lowerRequirement.Reason);
            Assert.Equal("description1-1", lowerRequirement.Description);
            var group = lowerRequirement.SpecificationGroups[0];
            Assert.Equal("<仕様グループ>", group.Category);
            var specifications = group.Specifications;
            Assert.False(specifications[0].IsImplemented);
            Assert.Equal("SPC01-01-01", specifications[0].ID);
            Assert.Equal("specification1", specifications[0].Description);

            Assert.True(specifications[1].IsImplemented);
            Assert.Equal("SPC01-01-02", specifications[1].ID);
            Assert.Equal("specification2", specifications[1].Description);
        }
    }
}
