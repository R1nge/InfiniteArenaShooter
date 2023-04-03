using UnityEngine;
using Zenject;

namespace DI
{
    public class WalletInstaller : MonoInstaller
    {
        [SerializeField] private Wallet walletPrefab;

        public override void InstallBindings()
        {
            var walletInstance = Container.InstantiatePrefabForComponent<Wallet>(walletPrefab);
            Container.Bind<Wallet>().FromInstance(walletInstance).AsSingle();
            Container.QueueForInject(walletPrefab);
        }
    }
}