namespace SensorSphereLearning.Services
{
    public class AppState
    {
        private int TotalSensors
        {
            get => _totalSensors;
            private set => _totalSensors = value;
        }

        private event Action? OnChange;

        public void SetTotalSensors(int total)
        {
            _totalSensors = total;
            NotifyStateChange();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
