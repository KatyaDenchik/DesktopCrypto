using CommunityToolkit.Mvvm.ComponentModel;
using DesktopCrypto.Enums;
using DesktopCrypto.Localizations;
using DesktopCrypto.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCrypto.ViewModel
{
    public partial class MainVM: ObservableObject
    {
        private readonly AppConfig appConfig;
        [ObservableProperty]
        private MainWindowLocalization? localization;
        
        [ObservableProperty]
        public Language language;
        public MainVM()
        {
        }
        public MainVM(AppConfig appConfig, LocalizationService localizationService)
        {
            this.appConfig = appConfig;
            Localization = new MainWindowLocalization();
            localizationService.LocalizationServiceChanged += () => Localization = new();
        }

        partial void OnLanguageChanged(Language value)
        {
            appConfig.Language = value;
        }
    }
}
