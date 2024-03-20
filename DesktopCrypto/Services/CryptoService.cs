using DesktopCrypto.Model;
using DesktopCrypto.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopCrypto.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly HttpClient httpClient;

        public CryptoService(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<CoinModel>> GetTopCoinsAsync(int topN)
        {
            try
            {
                string apiUrl = $"https://api.coingecko.com/api/v3/search/trending?per_page={topN}";
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


        public async Task<CoinModel> GetCoinDetailAsync(string coinId)
        {
            string apiUrl = $"https://api.coingecko.com/api/v3/coins/{coinId}";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);

                CoinModel coinDetail = new CoinModel
                {
                    Name = jsonResult["name"].ToString()
                };

                return coinDetail;
            }
            else
            {
                throw new HttpRequestException($"Failed to retrieve data from API: {response.ReasonPhrase}");
            }
        }

    }
}
