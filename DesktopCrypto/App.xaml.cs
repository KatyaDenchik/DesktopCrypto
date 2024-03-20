using DesktopCrypto.Services;
using DesktopCrypto.Services.Abstract;
using DesktopCrypto.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Net.Http;
using System.Windows;

namespace DesktopCrypto
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //private static readonly HttpClient httpClient = new HttpClient();
        public App()
        {
            Services = ConfigureServices();

            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<LocalizationService>();
            services.AddSingleton<AppConfig>();
            services.AddSingleton<MainVM>();
            services.AddSingleton<HttpClient>();
            services.AddSingleton<ICryptoService, CryptoService>();

            return services.BuildServiceProvider();
        }
    }

}
