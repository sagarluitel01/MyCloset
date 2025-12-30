using System;

namespace MyCloset.Services
{
    public class LoadingService
    {
        public event Action? OnChanged;

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                if (_isLoading == value)
                    return;
                _isLoading = value;
                OnChanged?.Invoke();
            }
        }

        public void Show() => IsLoading = true;
        public void Hide() => IsLoading = false;
    }
}
