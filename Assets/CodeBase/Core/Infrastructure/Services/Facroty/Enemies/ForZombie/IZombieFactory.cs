using CodeBase.GameLogic;
using UnityEngine;

namespace CodeBase.Core.Infrastructure.Services.Facroty.Enemies.ForZombie
{
    public interface IZombieFactory : IService
    {
        void SetTarget(Golem target);
        Zombie CreateArmorless(Vector3 position);
        Zombie CreateMetal(Vector3 position);
        Zombie CreateDiamond(Vector3 position);
        Zombie CreateNetherite(Vector3 position);
    }
}