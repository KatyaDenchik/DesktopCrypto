using DesktopCrypto.Commands;
using DesktopCrypto.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DesktopCrypto.Services
{
    public class LocalizationService
    {
        public event Action? LocalizationServiceChanged;
       
        public static void ChangeLocaization(object language)
        {
            if (language is Language lang)
            {
                ChangeLocaization(lang);
            }
            else
            {
                //Logger.Log("Failed to set localization");
            }
        }

        public void ChangeLocaization(Language language)
        {
            string culture = language switch
            {
                //Language.Russian => "ru-RU",
                Language.Ukrainian => "uk-UA",
                Language.English => "en-EN",
                _ => "uk-UA",
            };

            ChangeLocaization(culture);
        }
        public void ChangeLocaization(string language)
        {

            var cultureInfo = new CultureInfo(language);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;

            LocalizationServiceChanged?.Invoke();
        }


    }
}
