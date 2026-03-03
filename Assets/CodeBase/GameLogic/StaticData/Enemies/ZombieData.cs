using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.GameLogic.StaticData.Enemies
{
    [CreateAssetMenu(fileName = "ZombieData", menuName = "Game/Zombie Data", order = 0)]
    public class ZombieData : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _armorKickCount;
        [SerializeField] private float _flyAwayDistance;
        [SerializeField] private List<AudioClip> _bodyKickSounds;
        [SerializeField] private List<AudioClip> _armorKickSounds;


        public Sprite Sprite => _sprite;
        public int ArmorKickCount => _armorKickCount;
        public float FlyAwayDistance => _flyAwayDistance;
        public List<AudioClip> BodyKickSounds => _bodyKickSounds;
        public List<AudioClip> ArmorKickSounds => _armorKickSounds;
    }
}