namespace UsdmConverter.ApplicationCore.Entities
{
    /// <summary>
    /// Specification entity
    /// </summary>
    public class Specification
    {
        public bool IsImplemented { get; set; }
        public string ID { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
