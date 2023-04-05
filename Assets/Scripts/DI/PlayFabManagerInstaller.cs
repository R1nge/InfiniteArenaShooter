using PlayFab;
using UnityEngine;
using Zenject;

namespace DI
{
    public class PlayFabManagerInstaller : MonoInstaller
    {
        [SerializeField] private PlayFabManager playFab;

        public override void InstallBindings()
        {
            var playFabInstance = Container.InstantiatePrefabForComponent<PlayFabManager>(playFab);
            Container.Bind<PlayFabManager>().FromInstance(playFabInstance).AsSingle();
            Container.QueueForInject(playFab);
        }
    }
}