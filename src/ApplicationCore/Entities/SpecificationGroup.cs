using System.Collections.Generic;

namespace UsdmConverter.ApplicationCore.Entities
{
    /// <summary>
    /// Specification group entity
    /// </summary>
    public class SpecificationGroup
    {
        public string Category { get; set; } = string.Empty;
        public List<Specification> Specifications { get; set; } = new List<Specification>();
    }
}
