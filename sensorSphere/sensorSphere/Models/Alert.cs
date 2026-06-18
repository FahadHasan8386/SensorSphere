namespace sensorSphere.Models
{
    public class Alert
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty; // "HighTemperature", "HighCO", "DeviceOffline"
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public bool IsAcknowledged { get; set; }
    }
}
