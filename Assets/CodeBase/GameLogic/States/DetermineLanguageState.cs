using CodeBase.Core.Infrastructure.Services.GamePlatform;
using CodeBase.Core.Infrastructure.Services.Localization;
using CodeBase.Core.Infrastructure.States;

namespace CodeBase.GameLogic.States
{
    public class DetermineLanguageState : IndependentState
    {
        private readonly IGamePlatform _platform;
        private readonly ILocalizationService _localizationService;

        public DetermineLanguageState(IGamePlatform platform, ILocalizationService localizationService, StateMachine stateMachine) : base(stateMachine)
        {
            _platform = platform;
            _localizationService = localizationService;
        }

        public override void Enter()
        {
            _localizationService.SetLanguage(_platform.Language);
            StateMachine.Enter<LoadMenuState>();
        }
    }
}