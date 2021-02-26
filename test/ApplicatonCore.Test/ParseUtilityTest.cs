using System.Linq;
using Microsoft.Toolkit.Parsers.Markdown;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;
using Microsoft.Toolkit.Parsers.Markdown.Inlines;
using UsdmConverter.ApplicationCore.Entities;
using UsdmConverter.ApplicationCore.Logic;
using Xunit;
using Xunit.Sdk;

namespace UsdmConverter.ApplicatonCore.Test
{
    public class ParseUtilityTest
    {
        private MarkdownBlock CreateBlock(string markdown)
        {
            var document = new MarkdownDocument();
            document.Parse(markdown);
            return document.Blocks.First();
        }

        [Fact]
        public void DecomposeHeadingCorrectly()
        {
            var expectId = "REQ01";
            var expectSummary = "requirement";

            var header = CreateBlock($"## [{expectId}]{expectSummary}") as HeaderBlock;
            if (header != null)
            {
                var (actualId, actualSummary) = ParseUtility.DecomposeHeading(header);

                Assert.Equal(expectId, actualId);
                Assert.Equal(expectSummary, actualSummary);
            }
            else
            {
                throw new XunitException("Preparation failed");
            }
        }

        [Fact]
        public void DecomposeSpecificationCorrectly()
        {
            var expectId = "SPC01-01-01";
            var expectDescription = "requirement";
            var markdown = @$"* [x] [{expectId}]{expectDescription}";
            var block = CreateBlock(markdown) as ListBlock;
            if (block != null)
            {
                var actual = ParseUtility.DecomposeSpecification(block);

                Assert.True(actual[0].IsImplemented);
                Assert.Equal(expectId, actual[0].ID);
                Assert.Equal(expectDescription, actual[0].Description);
            }
            else
            {
                throw new XunitException("Preparation failed");
            }
        }
    }
}
