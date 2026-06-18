
using sensorSphere.Models;

namespace sensorSphere.Services
{
    public class DeviceStateService
    {
        private List<Device> _devices = new();

        public DeviceStateService()
        {
            // Seed demo devices
            _devices = new List<Device>
            {
                new Device { Id = 1, Name = "ESP32 Greenhouse", Location = "Greenhouse A", Status = "Online", LastSeen = DateTime.Now.AddMinutes(-2) },
                new Device { Id = 2, Name = "ESP32 Lab", Location = "Lab B", Status = "Online", LastSeen = DateTime.Now.AddMinutes(-5) },
                new Device { Id = 3, Name = "ESP32 Warehouse", Location = "Warehouse C", Status = "Offline", LastSeen = DateTime.Now.AddHours(-3) }
            };
        }

        public IReadOnlyList<Device> Devices => _devices.AsReadOnly();

        public event Action? OnChange; // Notify components when devices change

        // CRUD
        public Task AddDeviceAsync(Device device)
        {
            device.Id = _devices.Any() ? _devices.Max(d => d.Id) + 1 : 1;
            _devices.Add(device);
            NotifyStateChanged();
            return Task.CompletedTask;
        }

        public Task UpdateDeviceAsync(Device updatedDevice)
        {
            var existing = _devices.FirstOrDefault(d => d.Id == updatedDevice.Id);
            if (existing != null)
            {
                existing.Name = updatedDevice.Name;
                existing.Location = updatedDevice.Location;
                existing.Status = updatedDevice.Status;
                NotifyStateChanged();
            }
            return Task.CompletedTask;
        }

        public Task DeleteDeviceAsync(int id)
        {
            var device = _devices.FirstOrDefault(d => d.Id == id);
            if (device != null)
            {
                _devices.Remove(device);
                NotifyStateChanged();
            }
            return Task.CompletedTask;
        }

        public Device? GetDeviceById(int id) => _devices.FirstOrDefault(d => d.Id == id);

        public Task ToggleDeviceStatusAsync(int id)
        {
            var device = GetDeviceById(id);
            if (device != null)
            {
                device.Status = device.Status == "Online" ? "Offline" : "Online";
                device.LastSeen = DateTime.Now;
                NotifyStateChanged();
            }
            return Task.CompletedTask;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}