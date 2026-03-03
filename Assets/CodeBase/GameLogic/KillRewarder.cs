using System.Collections;
using System.Collections.Generic;
using CodeBase.GameUI;
using UnityEngine;

namespace CodeBase.GameLogic
{
    [RequireComponent(typeof(Collider2D))]
    public class KillRewarderZone : MonoBehaviour
    {
        [SerializeField] private Score _score;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private List<AudioClip> _rewardClips;
        [SerializeField] private float _delay;

        private void OnTriggerEnter2D(Collider2D other)
        {
            StartCoroutine(PlayRewardWithDelay());
        }

        private IEnumerator PlayRewardWithDelay()
        {
            yield return new WaitForSeconds(_delay);

            if (_rewardClips == null || _rewardClips.Count == 0 || _audioSource == null)
                yield break;

            AudioClip clip = _rewardClips[Random.Range(0, _rewardClips.Count)];
            _audioSource.PlayOneShot(clip);

            _score.Increase();
        }
    }
}