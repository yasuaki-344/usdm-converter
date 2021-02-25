using UsdmConverter.ApplicationCore.Logic;
using Xunit;

namespace UsdmConverter.ApplicatonCore.Test
{
    public class ParseUtilityTest
    {
        [Fact]
        public void DecomposeHeadingCorrectly()
        {
            var expectId = "REQ01";
            var expectSummary = "requirement";
            var input = $" [{expectId}]{expectSummary}";

            var (actualId, actualSummary) = ParseUtility.DecomposeHeading(input);

            Assert.Equal(expectId, actualId);
            Assert.Equal(expectSummary, actualSummary);
        }
    }
}
