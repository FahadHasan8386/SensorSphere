namespace sensorSphere.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Status { get; set; } = "Offline";
        public DateTime LastSeen { get; set; } = DateTime.Now;
    }
}
