using System.Collections.Generic;

namespace UsdmConverter.ApplicationCore.Entities
{
    /// <summary>
    /// Upper requirement entity
    /// </summary>
    public class UpperRequirement
    {
        public string ID { get; set; } = string.Empty;
        public string Summay { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<LowerRequirement> Requirements { get; set; } = new List<LowerRequirement>();
    }
}
