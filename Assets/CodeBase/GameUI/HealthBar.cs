using System;
using CodeBase.GameLogic;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.GameUI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private RectTransform _view;
        [SerializeField] private ProgressBarPro _progressBar;
        private Health _health;

        public void Show()
        {
            _view.DOLocalMoveY(0, 1).SetEase(Ease.InOutElastic);
        }

        public void BindHealth(Health health)
        {
            _health = health;
            _health.Changed += UpdateProgressBar;
        }

        private void UpdateProgressBar(int health, int maxHealth)
        {
            _progressBar.Value = (float)health / maxHealth;
        }
    }
}