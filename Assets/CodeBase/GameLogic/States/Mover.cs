using CodeBase.Core.Extensions;
using UnityEngine;

namespace CodeBase.GameLogic.States
{
    public class Mover : MonoBehaviour
    {
        private const float ReachDistance = 0.01f;
        
        [SerializeField] private float _speed;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Transform _target;
        private bool _isMoving;

        private float XDistanceToTarget => _target != null ? Mathf.Abs(transform.position.x - _target.position.x) : float.MaxValue;

        public void Construct(Transform target)
        {
            _target = target;
        }

        private void FixedUpdate()
        {
            if (!_isMoving || _target == null)
                return;

            float step = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _target.position, step).WithY(transform.position.y);

            if (XDistanceToTarget < ReachDistance)
                _isMoving = false;
        }

        public void Go()
        {
            _isMoving = true;
        }

        public void Stop()
        {
            _isMoving = false;
        }
    }
}