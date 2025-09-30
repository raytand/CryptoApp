using CryptoApp.ViewModels;
using System.Windows;

namespace CryptoApp.Views
{
    public partial class MainPage: Window
    {
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            Loaded += async (_, _) => await vm.LoadCoins();
        }
    }
}
