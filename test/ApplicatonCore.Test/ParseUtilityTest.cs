using System.Linq;
using Microsoft.Toolkit.Parsers.Markdown;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;
using UsdmConverter.ApplicationCore.Logic;
using Xunit;
using Xunit.Sdk;

namespace UsdmConverter.ApplicatonCore.Test
{
    public class ParseUtilityTest
    {
        [Fact]
        public void DecomposeHeadingCorrectly()
        {
            var expectId = "REQ01";
            var expectSummary = "requirement";

            var document = new MarkdownDocument();
            document.Parse($"## [{expectId}]{expectSummary}");
            var header = document.Blocks.First() as HeaderBlock;
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
    }
}
