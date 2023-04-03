using Player;
using UnityEngine;
using Zenject;

namespace DI
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerCharacter playerCharacter;
        [SerializeField] private Transform spawnPoint;
        
        public override void InstallBindings()
        {
            var playerInstance = Container.InstantiatePrefabForComponent<PlayerCharacter>(playerCharacter);
            playerInstance.transform.position = spawnPoint.position;
            Container.Bind<PlayerCharacter>().FromInstance(playerInstance).AsSingle();
            Container.QueueForInject(playerCharacter);
        }
    }
}