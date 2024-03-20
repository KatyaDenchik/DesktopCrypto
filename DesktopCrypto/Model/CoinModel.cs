using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopCrypto.CustomControls;
using DesktopCrypto.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace DesktopCrypto.Model
{
    public partial class CoinModel : ObservableObject
    {
        public string Id { get; set; }
        public int CoinId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public int MarketCapRank { get; set; }
        public string Thumb { get; set; }
        public string Small { get; set; }
        public string Large { get; set; }
        public string Slug { get; set; }
        public double PriceBTC { get; set; }
        public double Score { get; set; }
        public CoinData Data { get; set; }

        [RelayCommand]
        private void ShowDetails(MainPage page)
        {
            page.NavigationService.Navigate(new CoinInfoPage());
        }
    }

    public class CoinData
    {
        public string Price { get; set; }
        public string PriceBTC { get; set; }
        public Dictionary<string, double> PriceChangePercentage24h { get; set; }
        public string MarketCap { get; set; }
        public string MarketCapBTC { get; set; }
        public string TotalVolume { get; set; }
        public string TotalVolumeBTC { get; set; }
        public string Sparkline { get; set; }
        public object Content { get; set; } // Можете заменить на конкретный тип данных
    }
}
