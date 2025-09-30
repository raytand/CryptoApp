using CryptoApp.Services;
using CryptoApp.ViewModels;
using CryptoApp.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CryptoApp
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();

            services.AddHttpClient("CoinGecko", client =>
            {
                client.BaseAddress = new Uri(config["CoinGeckoService:API_BASE_URL"]);
            });
            
            services.AddSingleton<IConfiguration>(config);

            services.AddTransient<ICoinService, CoinGeckoService>();

            services.AddTransient<MainViewModel>();
            services.AddTransient<CoinDetailViewModel>();

            services.AddTransient<MainPage>();
            services.AddTransient<DetailPage>();

            Services = services.BuildServiceProvider();

            var window = Services.GetRequiredService<MainPage>();
            window.Show();

        }
    }

}
