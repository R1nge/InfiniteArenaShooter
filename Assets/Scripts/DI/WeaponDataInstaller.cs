using PlayerWeapons;
using UnityEngine;
using Zenject;

namespace DI
{
    public class WeaponDataInstaller : MonoInstaller
    {
        [SerializeField] private WeaponDataManager weaponDataManager;

        public override void InstallBindings()
        {
            var weaponDataManagerInstance = Container.InstantiatePrefabForComponent<WeaponDataManager>(weaponDataManager);
            Container.Bind<WeaponDataManager>().FromInstance(weaponDataManagerInstance).AsSingle();
            Container.QueueForInject(weaponDataManager);
        }
    }
}