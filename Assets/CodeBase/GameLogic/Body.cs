using DG.Tweening;

namespace CodeBase.GameLogic
{
    using UnityEngine;

    public class Body : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _partsRoot;
        [SerializeField] private Transform _leftHand;
        [SerializeField] private Transform _rightHand;

        [Header("View")]
        [SerializeField] private float _flashDuration = 0.5f;
        [SerializeField] private float _attackDuration = 0.25f;
        [SerializeField] private float _relaxDuration = 0.5f;
        [SerializeField] private float _leftHandAngle = 143f;
        [SerializeField] private float _rightHandAngle = 155f;
        [SerializeField] private Color _flashColor = new Color(1f, 0.4f, 0.4f);

        private SpriteRenderer[] _renderers;
        private Color[] _originalColors;

        public void Construct()
        {
            _renderers = _partsRoot.GetComponentsInChildren<SpriteRenderer>();
            _originalColors = new Color[_renderers.Length];

            for (int i = 0; i < _renderers.Length; i++)
                _originalColors[i] = _renderers[i].color;
        }

        public void Show() => _partsRoot.gameObject.SetActive(true);
        public void Hide() => _partsRoot.gameObject.SetActive(false);

        public void FlashRedTwice()
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                var renderer = _renderers[i];
                var original = _originalColors[i];

                Sequence seq = DOTween.Sequence();
                seq.Append(renderer.DOColor(_flashColor, _flashDuration))
                    .Append(renderer.DOColor(original, _flashDuration))
                    .Append(renderer.DOColor(_flashColor, _flashDuration))
                    .Append(renderer.DOColor(original, _flashDuration));
            }
        }

        public void Attack()
        {
            AnimateHand(_rightHand, _rightHandAngle);
            AnimateHand(_leftHand, _leftHandAngle);
        }

        private void AnimateHand(Transform hand, float angle)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(hand.DORotate(new Vector3(0, 0, angle), _attackDuration))
                .Append(hand.DORotate(Vector3.zero, _relaxDuration))
                .Play();
        }
    }
}