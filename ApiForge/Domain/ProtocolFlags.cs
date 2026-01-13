namespace ApiForge.Models
{
    [Flags]
    public enum ProtocolFlags
    {
        Http1 = 1,
        Http2 = 2,
        WebSocket = 4,
        Grpc = 8
    }
}
