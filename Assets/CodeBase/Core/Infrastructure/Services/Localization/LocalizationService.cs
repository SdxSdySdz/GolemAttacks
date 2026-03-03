using Lean.Localization;

namespace CodeBase.Core.Infrastructure.Services.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private readonly LeanLocalization _localization;

        public LocalizationService(LeanLocalization localization)
        {
            _localization = localization;
        }
        
        public void SetLanguage(Language language)
        {
            _localization.CurrentLanguage = language.ToString();
        }
    }
}