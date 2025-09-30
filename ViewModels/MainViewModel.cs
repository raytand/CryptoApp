using CryptoApp.Helpers;
using CryptoApp.Services;
using CryptoApp.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CryptoApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ICoinService _coinService;
        private List<Coin> _allCoins = new();
        public ObservableCollection<Coin> Coins { get; } = new();

        public ICommand Refresh { get; }
        public ICommand OpenDetailsCommand { get; }
        public ICommand SearchCommand { get; }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private string _search;
        public string Search
        {
            get => _search;
            set
            {
                if (_search == value) return;
                _search = value;
                OnPropertyChanged();
                _ = FilterCoinsAsync();
            }
        }

        public MainViewModel(ICoinService coinService)
        {
            _coinService = coinService;

            Refresh = new RelayCommand(LoadCoins);
            OpenDetailsCommand = new RelayCommand<string>(OpenDetails);
            SearchCommand = new RelayCommand(FilterCoinsAsync);
        }

        public async Task LoadCoins()
        {
            IsLoading = true;
            Coins.Clear();
            _allCoins.Clear();

            var top = await _coinService.GetTopCoinsAsync();
            foreach (var coin in top)
            {
                Coins.Add(coin);
                _allCoins.Add(coin);
            }

            IsLoading = false;
        }

        private Task OpenDetails(string id)
        {
            var vm = App.Services.GetRequiredService<CoinDetailViewModel>();

            var window = new DetailPage(vm, id);
            window.Show();

            return Task.CompletedTask;
        }

        private Task FilterCoinsAsync()
        {
            var text = (Search ?? string.Empty).Trim().ToLower();
            List<Coin> list = string.IsNullOrWhiteSpace(text)
                ? _allCoins
                : _allCoins.Where(c =>
                    (c.Name ?? string.Empty).ToLower().Contains(text) ||
                    (c.Symbol ?? string.Empty).ToLower().Contains(text)).ToList();

            Coins.Clear();
            foreach (var c in list)
            {
                Coins.Add(c);
            }
            return Task.CompletedTask;
        }
    }
}
