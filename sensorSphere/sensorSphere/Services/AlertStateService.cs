
using sensorSphere.Models;
using sensorSphere.Services;

namespace sensorSphere.Services
{
    public class AlertStateService
    {
        private List<Alert> _alerts = new();

        public AlertStateService(DeviceStateService deviceService, SensorStateService sensorService)
        {
            GenerateAlerts(deviceService, sensorService);
        }

        public IReadOnlyList<Alert> Alerts => _alerts.AsReadOnly();

        public event Action? OnChange;

        public void GenerateAlerts(DeviceStateService deviceService, SensorStateService sensorService)
        {
            _alerts.Clear();

            // High temperature alert (DHT22 > 30°C)
            foreach (var device in deviceService.Devices)
            {
                var dht = sensorService.GetLatestReading(device.Id, "DHT22");
                if (dht != null && dht.Temperature > 30)
                {
                    _alerts.Add(new Alert
                    {
                        Id = _alerts.Count + 1,
                        Type = "HighTemperature",
                        Message = $"High temperature ({dht.Temperature}°C) at {device.Name}",
                        Timestamp = DateTime.Now,
                        IsAcknowledged = false
                    });
                }
            }

            // High CO alert (MQ7 > 15 ppm)
            foreach (var device in deviceService.Devices)
            {
                var mq7 = sensorService.GetLatestReading(device.Id, "MQ7");
                if (mq7 != null && mq7.COLevel > 15)
                {
                    _alerts.Add(new Alert
                    {
                        Id = _alerts.Count + 1,
                        Type = "HighCO",
                        Message = $"High CO level ({mq7.COLevel} ppm) at {device.Name}",
                        Timestamp = DateTime.Now,
                        IsAcknowledged = false
                    });
                }
            }

            // Offline device alert
            foreach (var device in deviceService.Devices.Where(d => d.Status == "Offline"))
            {
                _alerts.Add(new Alert
                {
                    Id = _alerts.Count + 1,
                    Type = "DeviceOffline",
                    Message = $"Device {device.Name} is offline",
                    Timestamp = device.LastSeen,
                    IsAcknowledged = false
                });
            }

            NotifyStateChanged();
        }

        public Task AcknowledgeAlertAsync(int id)
        {
            var alert = _alerts.FirstOrDefault(a => a.Id == id);
            if (alert != null)
            {
                alert.IsAcknowledged = true;
                NotifyStateChanged();
            }
            return Task.CompletedTask;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}