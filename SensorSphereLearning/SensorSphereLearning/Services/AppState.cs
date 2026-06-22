namespace SensorSphereLearning.Services
{
    public class AppState
    {
        private int _totalSensors;

        public int TotalSensors
        {
            get => _totalSensors;
            private set => _totalSensors = value;
        }

        public event Action? OnChange;

        public void SetTotalSensors(int total)
        {
            _totalSensors = total;
            NotifyStateChanged(); 
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
