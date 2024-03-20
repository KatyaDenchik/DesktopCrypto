using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopCrypto.Enums;
using DesktopCrypto.Localizations;
using DesktopCrypto.Model;
using DesktopCrypto.Services;
using DesktopCrypto.Services.Abstract;
using ModernWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DesktopCrypto.ViewModel
{
    public partial class MainVM: ObservableObject
    {
        private readonly AppConfig appConfig;
        [ObservableProperty]
        private MainWindowLocalization? localization;

        public ICryptoService CryptoService { get; }
        public ObservableCollection<CoinModel> Coins { get; } = new ObservableCollection<CoinModel>();

        private CoinModel _selectedCoin;
        public CoinModel SelectedCoin
        {
            get => _selectedCoin;
            set => SetProperty(ref _selectedCoin, value);
        }

        [ObservableProperty]
        public Language language;
        public MainVM()
        {
        }
        public MainVM(ICryptoService cryptoService, AppConfig appConfig, LocalizationService localizationService)
        {
            CryptoService = cryptoService;
            this.appConfig = appConfig;
            Localization = new MainWindowLocalization();
            this.LoadDataAsync();
            localizationService.LocalizationServiceChanged += () => Localization = new();
        }

        partial void OnLanguageChanged(Language value)
        {
            appConfig.Language = value;
        }
        private bool isDark;

        [RelayCommand]
        private void SetDark()
        {
            if (isDark)
            {
                isDark = !isDark;
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                ThemeManager.Current.AccentColor = Colors.AliceBlue;
            }
            else
            {
                isDark = !isDark;
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                ThemeManager.Current.AccentColor = Colors.Green;
            }
        }
        public async Task LoadDataAsync()
        {
            try
            {
                int topN = 10;
                var topCoins = await CryptoService.GetTopCoinsAsync(topN);
                topCoins.ForEach(coin => Coins.Add(coin));
                // Обновление вашего пользовательского интерфейса с полученными данными
            }
            catch (Exception ex)
            {
                // Обработка ошибок при получении данных
            }
        }

          public async Task LoadCoinDetailAsync(string coinId)
        {
            try
            {
                SelectedCoin = await CryptoService.GetCoinDetailAsync(coinId);
            }
            catch (Exception ex)
            {
                // Обработка ошибок при получении данных о монете
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

    }
}
