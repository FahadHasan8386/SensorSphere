using SensorSphereLearning.Model;

namespace SensorSphereLearning.Services
{
    public class SensorService
    {
        private readonly List<Sensor> _sensors = new()
        {
            new Sensor { Id = 1, Name = "DHT22",  Temperature = 24.5, Humidity = 55, IsOnline = true  },
            new Sensor { Id = 2, Name = "MQ7",    Temperature = 26.0, Humidity = 40, IsOnline = true  },
            new Sensor { Id = 3, Name = "MQ136",  Temperature = 23.8, Humidity = 48, IsOnline = false },
        };


        private int _nextId = 4;


        public List<Sensor> GetSensors() => _sensors;

        public List<Sensor> GetRecentSensors(int count = 3) => 
            _sensors.OrderByDescending(s => s.Id).Take(count).ToList();


        public void AddSensor(string name , double temparature , double humidity)
        {
            _sensors.Add(new Sensor
            {
                Id = _nextId++,
                Name = name,
                Temperature = temparature,
                Humidity = humidity,
                IsOnline = true
            });
        }


        public void DeleteSensor(int id)
        {
            var sensor = _sensors.FirstOrDefault(s => s.Id == id);
            if(sensor is not null)
            {
                _sensors.Remove(sensor);
            }
        }

        public void ToggleStatus(int id)
        {
            var sensor = _sensors.FirstOrDefault(s => s.Id == id);
            if(sensor is not null)
            {
                sensor.IsOnline = !sensor.IsOnline;
            }
        }
    }
}
