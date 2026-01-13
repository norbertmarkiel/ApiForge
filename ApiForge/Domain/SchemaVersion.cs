namespace ApiForge.Models
{
    public class SchemaVersion
    {
        public int Version { get; set; }
        public SchemaDefinition Schema { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
