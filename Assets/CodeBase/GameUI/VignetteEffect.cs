using System;
using CodeBase.GameLogic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;

namespace CodeBase.GameUI
{
    public class VignetteEffect : MonoBehaviour
    {
        private const float MinIntensity = 0f;
        private const float MaxIntensity = 0.45f;
        private const float CriticalHealthPercentage = 0.45f;

        [SerializeField] private PostProcessVolume _postProcessVolume;
        [SerializeField] private float _tweenDuration = 0.5f;

        private Vignette _vignette;
        private Health _health;
        private Tween _intensityTween;

        public void Construct(Health health)
        {
            _health = health;
            _health.Changed += UpdateIntensity;
        }

        private void Awake()
        {
            _vignette = _postProcessVolume.profile.GetSetting<Vignette>();
        }

        private void OnDestroy()
        {
            if (_health != null)
                _health.Changed -= UpdateIntensity;

            _intensityTween?.Kill();
        }

        private void UpdateIntensity(int health, int maxHealth)
        {
            float healthPercentage = (float)health / maxHealth;
            float targetIntensity;

            if (healthPercentage > CriticalHealthPercentage)
                targetIntensity = MinIntensity;
            else
            {
                float intensityPercentage = healthPercentage / CriticalHealthPercentage;
                targetIntensity = Mathf.Lerp(MaxIntensity, MinIntensity, intensityPercentage);
            }

            _intensityTween?.Kill();
            _intensityTween = DOTween.To(
                () => _vignette.intensity.value,
                x => _vignette.intensity.value = x,
                targetIntensity,
                _tweenDuration
            ).SetEase(Ease.Linear);
        }
    }
}
