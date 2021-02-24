using UsdmConverter.ApplicationCore.Entities;

namespace UsdmConverter.ApplicationCore.Interfaces
{
    public interface IMarkdownParser
    {
        RequirementSpecification Parse(string content);
    }
}
