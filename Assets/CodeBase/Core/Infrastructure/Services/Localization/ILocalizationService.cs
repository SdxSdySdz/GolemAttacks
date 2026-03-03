namespace CodeBase.Core.Infrastructure.Services.Localization
{
    public interface ILocalizationService : IService
    {
        void SetLanguage(Language language);
    }
}