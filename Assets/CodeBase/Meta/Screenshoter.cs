#if UNITY_EDITOR
using System.IO;
using UnityEngine;

namespace CodeBase.Meta
{
    public class Screenshoter : MonoBehaviour
    {
        [SerializeField] private bool _isEnabled;
        [SerializeField] private float _cooldown = 0.5f;
        [SerializeField] private int _superSize = 4; // множитель для увеличения разрешения

        private float _timer;
        private int _screenshotIndex = 0;
        private string _folderPath;

        private void Start()
        {
            _folderPath = Path.Combine(Application.dataPath, "../Screenshots");

            if (!Directory.Exists(_folderPath))
                Directory.CreateDirectory(_folderPath);
        }

        private void Update()
        {
            if (_isEnabled == false)
                return;
            
            _timer += Time.deltaTime;

            if (_timer >= _cooldown)
            {
                TakeScreenshot();
                _timer = 0f;
            }
        }

        private void TakeScreenshot()
        {
            string filename = $"screenshot_{_screenshotIndex}.png";
            string fullPath = Path.Combine(_folderPath, filename);

            ScreenCapture.CaptureScreenshot(fullPath, _superSize);
            Debug.Log("Screenshot saved: " + fullPath);

            _screenshotIndex++;
        }
    }
}
#endif
