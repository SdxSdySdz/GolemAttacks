using CodeBase.Core.Infrastructure.Services.Localization;

namespace CodeBase.Core.Infrastructure.Services.GamePlatform
{
    public interface IGamePlatform : IService
    {
        void ShowInterstitialAdvertisement();
        Language Language { get; }
    }
}