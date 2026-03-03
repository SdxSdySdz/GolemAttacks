using CodeBase.Core.Infrastructure.Services.Localization;
using YG;

namespace CodeBase.Core.Infrastructure.Services.GamePlatform
{
    public class YandexGamesPlatform : IGamePlatform
    {
        public Language Language => GetLanguage();

        public void ShowInterstitialAdvertisement()
        {
            YG2.InterstitialAdvShow();
        }

        private Language GetLanguage()
        {
            return YG2.envir.language switch
            {
                "en" => Language.English,
                "ru" => Language.Russian,
                "tr" => Language.Turkish,
                _ => Language.English
            };
        }
    }
}