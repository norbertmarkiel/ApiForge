namespace ApiForge.Models
{
    public class EndpointDefinition
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }   // np. /orders/create
        public EndpointStatus Status { get; set; }

        public ProtocolFlags Protocols { get; set; }
        public RequestRateLimit RateLimit { get; set; }

        public SchemaDefinition Schema { get; set; }

        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; set; }
    }
}
