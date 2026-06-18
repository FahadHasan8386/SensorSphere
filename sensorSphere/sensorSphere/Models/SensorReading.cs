namespace sensorSphere.Models
{
    public class SensorReading
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string SensorType { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double COLevel { get; set; }
        public double H2SLevel { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;

    }
}
