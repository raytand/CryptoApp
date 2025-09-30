using CryptoApp.Models;
using CryptoApp.Services;
using System.Collections.ObjectModel;

namespace CryptoApp.ViewModels
{
    public class CoinDetailViewModel : BaseViewModel
    {
        private readonly ICoinService _coinService;
        public CoinDetail Coin { get; set; }
        public ObservableCollection<MarketInfo> Markets { get; set; } = new();

        public CoinDetailViewModel(ICoinService coinService)
        {
            _coinService = coinService;
        }

        public async Task LoadCoinAsync(string id)
        {
            Coin = await _coinService.GetCoinDetailAsync(id);
            OnPropertyChanged(nameof(Coin));

            var markets = await _coinService.GetCoinMarketsAsync(id);
            Markets.Clear();
            foreach (var m in markets) 
            { 
                Markets.Add(m);
            }
        }
    }

}
