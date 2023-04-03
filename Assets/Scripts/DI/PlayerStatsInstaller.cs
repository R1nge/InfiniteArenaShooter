using UnityEngine;
using Zenject;

namespace DI
{
    public class PlayerStatsInstaller : MonoInstaller
    {
        [SerializeField] private PlayerStats playerStats;

        public override void InstallBindings()
        {
            var playerInstance = Container.InstantiatePrefabForComponent<PlayerStats>(playerStats);
            Container.Bind<PlayerStats>().FromInstance(playerInstance).AsSingle();
            Container.QueueForInject(playerStats);
        }
    }
}