using DesktopCrypto.Enums;
using DesktopCrypto.Helpers;
using DesktopCrypto.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCrypto
{
    public class AppConfig
    {
        public readonly string ConfigFilePath;
        private readonly LocalizationService localizationService;

        public IniParser ConfigFile { get; }
        public string PathToExecutableFolder => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private Language language;
        public Language Language 
        { 
            get =>  language;
            set { 
                language = value;
                ConfigFile.Write("UI", "Language", value);
                localizationService.ChangeLocaization(value);
            }
        }

        public AppConfig(LocalizationService localizationService)
        {
            ConfigFilePath = Path.Combine(PathToExecutableFolder, "Config.ini");
            ConfigFile = new IniParser(ConfigFilePath);
            this.localizationService = localizationService;
        }
    }
}
