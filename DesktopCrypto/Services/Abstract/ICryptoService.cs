using DesktopCrypto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCrypto.Services.Abstract
{
    public interface ICryptoService
    {
        Task<List<CoinModel>> GetTopCoinsAsync(int topN);
        Task<CoinModel> GetCoinDetailAsync(string coinId);
    }
}
