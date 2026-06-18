using sensorSphere.Models;

namespace sensorSphere.Services
{
    public class AuthStateService
    {
        private User? _currentUser;

        private readonly List<User> _demUsers = new()
        {
            new User { Email = "admin@atmosync.com", Password = "admin123", Role = "Admin", Name = "Admin" },
            new User { Email = "user@atmosync.com", Password = "user123", Role = "User", Name = "User" }
        };

        public User? CurrentUser => _currentUser;
        public bool IsAuthenticated => _currentUser != null;
        public event Action? OnChange;

        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = _demUsers.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                _currentUser = user;
                NotifyStateChanged();
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task LogoutAsync()
        {
            _currentUser = null;
            NotifyStateChanged();
            await Task.CompletedTask;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}