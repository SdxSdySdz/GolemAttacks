using System.Collections.Generic;
using CodeBase.Core.Extensions;
using CodeBase.GameLogic.States;
using CodeBase.GameLogic.StaticData.Enemies;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.GameLogic
{
    public class Zombie : MonoBehaviour, IKickable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Mover _mover;
        [SerializeField] private float _deathDuration = 1f;
        [SerializeField] private AudioSource _audioSource;
        
        private int _armorKickCount;
        private int _currentKickCount;
        private float _flyAwayDistance;
        private Sprite _armorlessSprite;
        private List<AudioClip> _bodyKickSounds;
        private List<AudioClip> _armorKickSounds;
        private Tween _flyAwayTween;
        private float _originalY;

        public int Damage => 1;

        public void Construct(Golem target, Sprite armorlessSprite, ZombieData data)
        {
            _armorlessSprite = armorlessSprite;
            _flyAwayDistance = data.FlyAwayDistance;
            _mover.Construct(target.ZombieTarget);
            _spriteRenderer.sprite = data.Sprite;
            _armorKickCount = data.ArmorKickCount;
            
            _bodyKickSounds = data.BodyKickSounds;
            _armorKickSounds = data.ArmorKickSounds;
            
            _currentKickCount = 0;
            _originalY = transform.position.y;
        }

        public void Go()
        {
            _mover.Go();
        }

        public void TakeDamage()
        {
            _mover.Stop();
            _currentKickCount++;
          
            if (_currentKickCount < _armorKickCount)
                FlyAway();
            else if (_currentKickCount == _armorKickCount)
                BreakArmor();
            else
                Die();
        }

        private void FlyAway()
        {
            _mover.Stop();

            _flyAwayTween?.Kill(complete: false);

            Vector3 jumpTarget = new Vector3(transform.position.x + _flyAwayDistance, _originalY, transform.position.z);
            _flyAwayTween = transform
                .DOJump(jumpTarget, 3, 1, 1)
                .SetEase(Ease.OutQuad)
                .OnComplete(_mover.Go);

            TryPlayArmorKickSound();
        }

        private void PlayBodyKickSound()
        {
            _audioSource.clip = _bodyKickSounds.GetRandomElement();
            _audioSource.Play();
        }

        private void TryPlayArmorKickSound()
        {
            if (_armorKickSounds.Count == 0)
                return;
            
            _audioSource.clip = _armorKickSounds.GetRandomElement();
            _audioSource.Play();
        }

        private void BreakArmor()
        {
            _spriteRenderer.sprite = _armorlessSprite;
            FlyAway();
        }

        private void Die()
        {
            _mover.Stop();
            
            float offsetX = UnityEngine.Random.Range(-5f, 0);
            float offsetY = UnityEngine.Random.Range(0f, 1);
            Vector3 target = transform.position + new Vector3(5, 8f) + new Vector3(offsetX, offsetY);
            
            transform
                .DOJump(target, 3, 1, 1)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => Destroy(gameObject));
            
            PlayBodyKickSound();
        }
    }
}