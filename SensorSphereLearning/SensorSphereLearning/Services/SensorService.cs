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


    }
}
