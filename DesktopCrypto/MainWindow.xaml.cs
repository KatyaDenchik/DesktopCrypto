using DesktopCrypto.Model;
using DesktopCrypto.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopCrypto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient httpClient;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<MainVM>();
            // Инициализация HttpClient при создании формы
            httpClient = new HttpClient();
            Loaded += MainWindow_Loaded;
        }

        // Метод для получения списка топ монет
        private async Task<List<CoinModel>> GetTopCoinsAsync()
        {
            try
            {
                string apiUrl = "https://api.coingecko.com/api/v3/search/trending";
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    dynamic jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);
                    List<CoinModel> topCoins = new List<CoinModel>();
                    foreach (var coinJson in jsonResult["coins"])
                    {
                        CoinModel coin = new CoinModel
                        {
                            Id = coinJson["item"]["id"].ToString(),
                            CoinId = (int)coinJson["item"]["coin_id"],
                            Name = coinJson["item"]["name"].ToString(),
                            Symbol = coinJson["item"]["symbol"].ToString(),
                            MarketCapRank = (int)coinJson["item"]["market_cap_rank"],
                            Thumb = coinJson["item"]["thumb"].ToString(),
                            Small = coinJson["item"]["small"].ToString(),
                            Large = coinJson["item"]["large"].ToString(),
                            Slug = coinJson["item"]["slug"].ToString(),
                            PriceBTC = (double)coinJson["item"]["price_btc"],
                            Score = (double)coinJson["item"]["score"],
                            Data = new CoinData
                            {
                                Price = coinJson["item"]["data"]["price"].ToString(),
                                PriceBTC = coinJson["item"]["data"]["price_btc"].ToString(),
                                MarketCap = coinJson["item"]["data"]["market_cap"].ToString(),
                                MarketCapBTC = coinJson["item"]["data"]["market_cap_btc"].ToString(),
                                TotalVolume = coinJson["item"]["data"]["total_volume"].ToString(),
                                TotalVolumeBTC = coinJson["item"]["data"]["total_volume_btc"].ToString(),
                                Sparkline = coinJson["item"]["data"]["sparkline"].ToString(),
                                Content = coinJson["item"]["data"]["content"]
                            }
                        };
                        topCoins.Add(coin);
                    }
                    return topCoins;
                }
                else
                {
                    MessageBox.Show("Ошибка при получении данных: " + response.ReasonPhrase);
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
                return null;
            }
        }



        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            List<CoinModel> topCoins = await GetTopCoinsAsync();
            if (topCoins != null)
            {
                foreach (var coin in topCoins)
                {
                    // Добавляем имена монет в список на главной форме
                    // coinsListView - это имя вашего ListView в XAML
                    //coinsListView.Items.Add(coin.Name);
                }
            }
        }
    }
}