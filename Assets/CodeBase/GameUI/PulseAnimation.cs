using DG.Tweening;
using UnityEngine;

namespace CodeBase.GameUI
{
    public class PulseAnimation : MonoBehaviour
    {
        [Header("Pulsate Settings")]
        [SerializeField] private float _scaleMultiplier = 1.2f; 
        [SerializeField] private float _duration = 0.5f; 

        private Vector3 originalScale;

        private void Start()
        {
            originalScale = transform.localScale;

            transform.DOScale(originalScale * _scaleMultiplier, _duration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        private void OnDisable()
        {
            transform.DOKill();
        }
    }
}