using sensorSphere.Models;

namespace sensorSphere.Services
{
    public class SensorStateService
    {
        private List<SensorReading> _readings = new();

        public SensorStateService()
        {
            var rnd = new Random();
            var devices = new[] { 1, 2, 3 }; 

            foreach (var deviceId in devices)
            {
                // DHT22 - Temperature and Humidity
                _readings.Add(new SensorReading
                {
                    Id = _readings.Count + 1,
                    DeviceId = deviceId,
                    SensorType = "DHT22",
                    Temperature = Math.Round(rnd.NextDouble() * 20 + 15, 1), // 15-35°C
                    Humidity = Math.Round(rnd.NextDouble() * 40 + 30, 1),   // 30-70%
                    COLevel = 0,
                    H2SLevel = 0,
                    Timestamp = DateTime.Now.AddMinutes(-rnd.Next(1, 60))
                });

                // MQ7 - CO
                _readings.Add(new SensorReading
                {
                    Id = _readings.Count + 1,
                    DeviceId = deviceId,
                    SensorType = "MQ7",
                    Temperature = 0,
                    Humidity = 0,
                    COLevel = Math.Round(rnd.NextDouble() * 20 + 5, 1), // 5-25 ppm
                    H2SLevel = 0,
                    Timestamp = DateTime.Now.AddMinutes(-rnd.Next(1, 60))
                });

                // MQ136 - H2S
                _readings.Add(new SensorReading
                {
                    Id = _readings.Count + 1,
                    DeviceId = deviceId,
                    SensorType = "MQ136",
                    Temperature = 0,
                    Humidity = 0,
                    COLevel = 0,
                    H2SLevel = Math.Round(rnd.NextDouble() * 5 + 1, 1), // 1-6 ppm
                    Timestamp = DateTime.Now.AddMinutes(-rnd.Next(1, 60))
                });
            }
        }

        public IReadOnlyList<SensorReading> Readings => _readings.AsReadOnly();

        public event Action? OnChange;

        // Get latest
        public SensorReading? GetLatestReading(int deviceId, string sensorType)
        {
            return _readings
                .Where(r => r.DeviceId == deviceId && r.SensorType == sensorType)
                .OrderByDescending(r => r.Timestamp)
                .FirstOrDefault();
        }

        // Get all
        public List<SensorReading> GetReadingsForDevice(int deviceId, string sensorType)
        {
            return _readings
                .Where(r => r.DeviceId == deviceId && r.SensorType == sensorType)
                .OrderBy(r => r.Timestamp)
                .ToList();
        }

        public Task AddReadingAsync(SensorReading reading)
        {
            reading.Id = _readings.Any() ? _readings.Max(r => r.Id) + 1 : 1;
            _readings.Add(reading);
            NotifyStateChanged();
            return Task.CompletedTask;
        }

        // Generate a new random 
        public Task GenerateDemoReadingAsync(int deviceId, string sensorType)
        {
            var rnd = new Random();
            var reading = new SensorReading
            {
                DeviceId = deviceId,
                SensorType = sensorType,
                Timestamp = DateTime.Now
            };

            switch (sensorType)
            {
                case "DHT22":
                    reading.Temperature = Math.Round(rnd.NextDouble() * 20 + 15, 1);
                    reading.Humidity = Math.Round(rnd.NextDouble() * 40 + 30, 1);
                    break;
                case "MQ7":
                    reading.COLevel = Math.Round(rnd.NextDouble() * 20 + 5, 1);
                    break;
                case "MQ136":
                    reading.H2SLevel = Math.Round(rnd.NextDouble() * 5 + 1, 1);
                    break;
            }
            return AddReadingAsync(reading);
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}