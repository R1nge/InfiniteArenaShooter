using UI;
using UnityEngine;
using Zenject;

namespace DI
{
    public class DeathScreenInstaller : MonoInstaller
    {
        [SerializeField] private DeathScreen deathScreen;
        public override void InstallBindings()
        {
            var deathScreenInstance = Container.InstantiatePrefabForComponent<DeathScreen>(deathScreen);
            Container.Bind<DeathScreen>().FromInstance(deathScreenInstance).AsSingle();
            Container.QueueForInject(deathScreen);
        }
    }
}