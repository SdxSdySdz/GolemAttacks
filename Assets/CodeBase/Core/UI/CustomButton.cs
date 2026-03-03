using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.Core.UI
{
    public class CustomButton : MonoBehaviour
    {
        private Button _button;

        public event UnityAction Clicked;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            if (_button == null)
                _button = GetComponentInChildren<Button>();

            OnAwake();
        }

        protected virtual void OnAwake()
        {
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(NotifyListeners);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(NotifyListeners);
        }

        protected void NotifyListeners()
        {
            Clicked?.Invoke();
        }
    }
}