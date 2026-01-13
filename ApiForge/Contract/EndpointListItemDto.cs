using ApiForge.Models;

namespace ApiForge.Contract
{
    public sealed class EndpointListItemDto
    {
        public string Name { get; init; }
        public string Path { get; init; }
        public EndpointStatus Status { get; init; }
        public ProtocolFlags Protocols { get; init; }
    }
}
