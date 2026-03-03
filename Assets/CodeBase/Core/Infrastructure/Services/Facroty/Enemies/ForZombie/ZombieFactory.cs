using System;
using CodeBase.GameLogic;
using CodeBase.GameLogic.StaticData.Enemies;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Core.Infrastructure.Services.Facroty.Enemies.ForZombie
{
    public class ZombieFactory : MonoBehaviour, IZombieFactory
    {
        [SerializeField] private Zombie _prefab;
        [SerializeField] private ZombieData _armorlessData;
        [SerializeField] private ZombieData _metalData;
        [SerializeField] private ZombieData _diamondData;
        [SerializeField] private ZombieData _netheriteData;
        
        private Golem _target;
        private IZombieFactory _zombieFactoryImplementation;

        public void SetTarget(Golem target)
        {
            _target = target;
        }
        
        public Zombie CreateArmorless(Vector3 position)
        {
            return CreateZombie(position, _armorlessData);
        }

        public Zombie CreateMetal(Vector3 position)
        {
            return CreateZombie(position, _metalData);
        }

        public Zombie CreateDiamond(Vector3 position)
        {
            return CreateZombie(position, _diamondData);
        }
        
        public Zombie CreateNetherite(Vector3 position)
        {
            return CreateZombie(position, _netheriteData);
        }

        private Zombie CreateZombie(Vector3 position, ZombieData data)
        {
            if (_target == null)
                throw new NullReferenceException(nameof(_target));

            Zombie instance = Object.Instantiate(_prefab, position, Quaternion.identity);
            instance.Construct(
                _target, 
                _armorlessData.Sprite,
                data);
            
            return instance;
        }
    }
}