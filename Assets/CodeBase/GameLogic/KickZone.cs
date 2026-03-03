using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.GameLogic
{
    public class KickZone : MonoBehaviour
    {
        [SerializeField] private Collider2D _zoneCollider;
        
        private ContactFilter2D _contactFilter;
        private List<Collider2D> _results;

        private void Awake()
        {
            _results = new List<Collider2D>();
            
            if (_zoneCollider == null)
                _zoneCollider = GetComponent<Collider2D>();
            _contactFilter = new ContactFilter2D
            {
                useTriggers = true
            };
        }

        public void Kick()
        {
            _results.Clear();
            _zoneCollider.OverlapCollider(_contactFilter, _results);
            foreach (var col in _results)
            {
                var kickable = col.GetComponent<IKickable>();
                if (kickable != null)
                    kickable.TakeDamage();
            }
        }
    }
}