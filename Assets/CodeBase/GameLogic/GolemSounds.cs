using System.Collections.Generic;
using CodeBase.Core.Extensions;
using UnityEngine;

namespace CodeBase.GameLogic
{
    public class GolemSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        [Header("Audio Clips")]
        [SerializeField] private List<AudioClip> _attackClips;
        [SerializeField] private List<AudioClip> _takeDamageClips;
        [SerializeField] private List<AudioClip> _deathClips;

        public void Attack()
        {
            PlayRandomFrom(_attackClips);
        }

        public void PlayTakeDamage()
        {
            PlayRandomFrom(_takeDamageClips);
        }

        public void PlayKill()
        {
            PlayRandomFrom(_deathClips);
        }

        private void PlayRandomFrom(List<AudioClip> clips)
        {
            if (_audioSource == null || clips == null || clips.Count == 0)
                return;
            
            _audioSource.PlayOneShot(clips.GetRandomElement());
        }
    }
}