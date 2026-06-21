namespace SensorSphereLearning.Model
{
    public class Sensor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public bool IsOnline { get; set; }
        public string Status => IsOnline ? "Online" : "Offline";
    }
}
