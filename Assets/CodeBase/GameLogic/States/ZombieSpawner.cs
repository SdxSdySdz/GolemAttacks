using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Core.Extensions;
using CodeBase.Core.Infrastructure.Services.Facroty.Enemies.ForZombie;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.GameLogic.States
{
    public class ZombieSpawner : MonoBehaviour
    {
        [SerializeField] private float _minCooldown;
        [SerializeField] private float _maxCooldown;

        private List<Zombie> _spawnedZombie;
        private Coroutine _spawnRoutine;
        private IZombieFactory _zombieFactory;
        private List<Func<Vector3, Zombie>> _spawnFunctions;

        private void Awake()
        {
            _spawnedZombie = new List<Zombie>();
        }

        public void Construct(Golem target, IZombieFactory zombieFactory)
        {
            zombieFactory.SetTarget(target);
            _zombieFactory = zombieFactory;

            _spawnFunctions = new List<Func<Vector3, Zombie>>()
            {
                _zombieFactory.CreateArmorless,
                _zombieFactory.CreateArmorless,
                _zombieFactory.CreateArmorless,
                _zombieFactory.CreateArmorless,
                _zombieFactory.CreateArmorless,
                _zombieFactory.CreateArmorless,
                _zombieFactory.CreateArmorless,
                _zombieFactory.CreateArmorless,
                _zombieFactory.CreateArmorless,
                _zombieFactory.CreateArmorless,
                _zombieFactory.CreateArmorless,
                _zombieFactory.CreateArmorless,
                _zombieFactory.CreateMetal,
                _zombieFactory.CreateMetal,
                _zombieFactory.CreateMetal,
                _zombieFactory.CreateMetal,
                _zombieFactory.CreateMetal,
                _zombieFactory.CreateMetal,
                _zombieFactory.CreateDiamond,
                _zombieFactory.CreateDiamond,
                _zombieFactory.CreateDiamond,
                _zombieFactory.CreateNetherite,
            };
            
            _spawnedZombie.Clear();
        }

        public void StartSpawning()
        {
            if (_spawnRoutine == null)
                _spawnRoutine = StartCoroutine(SpawnRoutine());
        }

        public void Clear()
        {
            if (_spawnRoutine != null)
                StopCoroutine(_spawnRoutine);
            
            foreach (Zombie zombie in _spawnedZombie)
            {
                if (zombie == null)
                    continue;
                
                Destroy(zombie.gameObject);
            }
            
            _spawnedZombie.Clear();
        }

        private IEnumerator SpawnRoutine()
        {
            yield return SpawnWarmingUpGroup();
            
            while (true)
            {
                SpawnZombie();
                
                float delay = Random.Range(_minCooldown, _maxCooldown);
                yield return new WaitForSeconds(delay);
            }
        }

        private IEnumerator SpawnWithRandomDelay(Func<Vector3, Zombie> spawn)
        {
            float delay = GenerateDelay();
            Zombie zombie = spawn(transform.position);
            _spawnedZombie.Add(zombie);
            zombie.Go();
            yield return new WaitForSeconds(delay);
        }

        private IEnumerator SpawnWarmingUpGroup()
        {
            Zombie zombie;
            float delay;
            
            delay = GenerateDelay();
            zombie = _zombieFactory.CreateArmorless(transform.position);
            _spawnedZombie.Add(zombie);
            zombie.Go();
            yield return new WaitForSeconds(delay);
            
            delay = GenerateDelay();
            zombie = _zombieFactory.CreateArmorless(transform.position);
            _spawnedZombie.Add(zombie);
            zombie.Go();
            yield return new WaitForSeconds(delay);
            
            delay = GenerateDelay();
            zombie = _zombieFactory.CreateDiamond(transform.position);
            _spawnedZombie.Add(zombie);
            zombie.Go();
            yield return new WaitForSeconds(delay);
        }

        private float GenerateDelay()
        {
            return Random.Range(_minCooldown, _maxCooldown);
        }

        private void SpawnZombie()
        {
            Func<Vector3, Zombie> spawnFunction = _spawnFunctions.GetRandomElement();
            
            var zombie = spawnFunction(transform.position);
            _spawnedZombie.Add(zombie);
            
            zombie.Go();
        }
    }
}