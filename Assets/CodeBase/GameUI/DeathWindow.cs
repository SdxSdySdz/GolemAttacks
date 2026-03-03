using System;
using CodeBase.Core.Infrastructure.Services.GamePlatform;
using CodeBase.Core.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace CodeBase.GameUI
{
    public class DeathWindow : MonoBehaviour
    {
        private const float HiddenScale = 0;
        private const float VisibleScale = 1;
        
        [SerializeField] private RectTransform _view;
        [SerializeField] private Image _background;
        [SerializeField] private RectTransform _window;
        [SerializeField] private CustomButton _restartButton;
        [SerializeField] private CustomButton _menuButton;
        
        private Color _hiddenBackgroundColor = new Color(0, 0, 0, 0);
        private Color _visibleBackgroundColor = new Color(0, 0, 0, 220f/256);

        private Action _onRestartRequested;
        private Action _onMenuRequested;

        private void Awake()
        {
            Hide();
        }

        public void Show(Action onRestartRequested, Action onMenuRequested)
        {
            _onMenuRequested = onMenuRequested;
            _onRestartRequested = onRestartRequested;
            _background.color = _hiddenBackgroundColor;
            _window.localScale = Vector3.one * HiddenScale;

            _restartButton.Clicked += OnRestartButtonClicked;
            _menuButton.Clicked += OnMenuButtonClicked;
            
            _view.gameObject.SetActive(true);
            _background.DOColor(_visibleBackgroundColor, 1);

            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(0.5f);
            sequence.Append(_window
                .DOScale(Vector3.one * VisibleScale, 1)
                .SetEase(Ease.OutBack));
        }

        public void Hide()
        {
            _view.gameObject.SetActive(false);
        }

        private void OnRestartButtonClicked()
        {
            _onRestartRequested?.Invoke();
        }

        private void OnMenuButtonClicked()
        {
            _onMenuRequested?.Invoke();
        }
    }
}