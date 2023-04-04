using PlayerWeapons;
using UnityEngine;
using Zenject;

namespace DI
{
    public class BulletPoolInstaller : MonoInstaller
    {
        [SerializeField] private BulletPool bulletPool;

        public override void InstallBindings()
        {
            var bulletPoolInstance = Container.InstantiatePrefabForComponent<BulletPool>(bulletPool);
            Container.Bind<BulletPool>().FromInstance(bulletPoolInstance).AsSingle();
            Container.QueueForInject(bulletPool);
        }
    }
}