using System;
using CodeBase.Core.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.GameUI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private PlayButton _playButton;
        [SerializeField] private HandHint _handHint;
        
        private IInputService _inputService;
        private bool _isWaitingForUserTouch;
        
        public event Action PlaySelected;

        private void Awake()
        {
            HideUI();
        }

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
            _playButton.Construct(inputService);
            _isWaitingForUserTouch = false;
        }

        private void Update()
        {
            if (_isWaitingForUserTouch == false)
                return;

            if (_inputService.IsClicked())
            {
                NotifyAboutGameRunning();
                _isWaitingForUserTouch = false;
            }
        }

        public void Show()
        {
            if (_inputService is StandaloneInputService)
                PrepareStandaloneMenu();
            else
                PrepareMobileMenu();
        }

        public void Hide()
        {
            HideUI();
            gameObject.SetActive(false);
        }

        private void PrepareStandaloneMenu()
        {
            _playButton.Show();
            _playButton.Clicked += NotifyAboutGameRunning;
        }

        private void PrepareMobileMenu()
        {
            _isWaitingForUserTouch = true;
            _handHint.Show();
        }

        private void NotifyAboutGameRunning()
        {
            PlaySelected?.Invoke();
        }


        private void HideUI()
        {
            _playButton.Hide();
            _handHint.Hide();
        }
    }
}