using SkillSystem;
using UnityEngine;
using Zenject;

namespace DI
{
    public class SkillLevelManagerInstaller : MonoInstaller
    {
        [SerializeField] private SkillLevelManager skillLevelManager;

        public override void InstallBindings()
        {
            var skillLevelManagerInstance = Container.InstantiatePrefabForComponent<SkillLevelManager>(skillLevelManager);
            Container.Bind<SkillLevelManager>().FromInstance(skillLevelManagerInstance).AsSingle();
            Container.QueueForInject(skillLevelManager);
        }
    }
}