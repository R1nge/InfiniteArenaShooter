using UnityEngine;
using Zenject;

namespace DI
{
    public class WaveSpawnerInstaller : MonoInstaller
    {
        [SerializeField] private WaveSpawner waveSpawner;

        public override void InstallBindings()
        {
            var waveSpawnerInstance = Container.InstantiatePrefabForComponent<WaveSpawner>(waveSpawner);
            Container.Bind<WaveSpawner>().FromInstance(waveSpawnerInstance).AsSingle();
            Container.QueueForInject(waveSpawner);
        }
    }
}