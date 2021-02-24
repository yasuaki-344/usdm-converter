using System.Collections.Generic;

namespace UsdmConverter.ApplicationCore.Entities
{
    /// <summary>
    /// Lower requirement entity
    /// </summary>
    public class LowerRequirement
    {
        public string ID { get; set; } = string.Empty;
        public string Summay { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<SpecificationGroup> SpecificationGroups { get; set; } = new List<SpecificationGroup>();
    }
}
