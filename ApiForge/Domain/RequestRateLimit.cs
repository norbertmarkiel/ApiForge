namespace ApiForge.Models
{
    public class RequestRateLimit
    {
        public int RequestsPerSecond { get; set; }

        public bool IsExceeded(int currentRps)
            => currentRps > RequestsPerSecond;
    }
}
