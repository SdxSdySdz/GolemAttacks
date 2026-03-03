using CodeBase.Core.Infrastructure.Services.Input;
using CodeBase.Core.UI;

namespace CodeBase.GameUI
{
    public class PlayButton : CustomButton
    {
        private IInputService _inputService;

        private void Update()
        {
            if (_inputService == null)
                return;

            if (_inputService.IsClicked())
                NotifyListeners();
        }

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}