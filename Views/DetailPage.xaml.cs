using CryptoApp.ViewModels;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace CryptoApp.Views
{
    public partial class DetailPage : Window
    {
        public DetailPage(CoinDetailViewModel vm, string coinId)
        {
            InitializeComponent();
            DataContext = vm;
            Loaded += async (_, _) => await vm.LoadCoinAsync(coinId);
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }
    }
}
