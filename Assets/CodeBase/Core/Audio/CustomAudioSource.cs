using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Core.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class CustomAudioSource : MonoBehaviour
    {
        private AudioSource _audioSource;
        private Coroutine _currentFadeCoroutine;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play(float fadingDuration = 0f)
        {
            if (fadingDuration <= 0f)
            {
                if (_audioSource.isPlaying)
                    return;
                
                _audioSource.volume = 1f;
                _audioSource.Play();
                return;
            }

            if (_currentFadeCoroutine != null)
                StopCoroutine(_currentFadeCoroutine);

            _currentFadeCoroutine = StartCoroutine(FadeIn(fadingDuration));
        }

        public void Stop(float fadingDuration = 0f, Action onFinished = null)
        {
            if (fadingDuration <= 0f)
            {
                _audioSource.Stop();
                onFinished?.Invoke();
                return;
            }

            if (_currentFadeCoroutine != null)
                StopCoroutine(_currentFadeCoroutine);

            _currentFadeCoroutine = StartCoroutine(FadeOut(fadingDuration, onFinished));
        }

        private IEnumerator FadeIn(float duration)
        {
            _audioSource.volume = 0f;
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                _audioSource.volume = Mathf.Clamp01(elapsedTime / duration);
                yield return null;
            }

            _audioSource.volume = 1f;
            _currentFadeCoroutine = null;
        }

        private IEnumerator FadeOut(float duration, Action onFinished = null)
        {
            float startingVolume = _audioSource.volume;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(startingVolume, 0f, elapsedTime / duration);
                yield return null;
            }

            _audioSource.Stop();
            _audioSource.volume = 1f;
            _currentFadeCoroutine = null;
            
            onFinished?.Invoke();
        }
    }
}
