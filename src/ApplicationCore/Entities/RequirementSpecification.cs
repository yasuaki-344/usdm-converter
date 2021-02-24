using System.Collections.Generic;

namespace UsdmConverter.ApplicationCore.Entities
{
    /// <summary>
    /// Requirement specification entity
    /// </summary>
    public class RequirementSpecification
    {
        public string Title { get; set; } = string.Empty;
        public List<UpperRequirement> Requirements { get; set; } = new List<UpperRequirement>();
    }
}
