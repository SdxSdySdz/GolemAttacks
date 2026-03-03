using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.GameUI
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _icon;

        private Tween _iconPunchTween;

        public int Value => Convert.ToInt32(_text.text);

        public void Increase()
        {
            _text.text = (Value + 1).ToString();

            if (_iconPunchTween != null && _iconPunchTween.IsActive())
            {
                _iconPunchTween.Kill();
            }

            _icon.transform.localScale = Vector3.one;
            _iconPunchTween = _icon.transform.DOPunchScale(_icon.transform.localScale * 0.3f, 0.2f);
        }
    }
}