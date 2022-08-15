using UnityEngine;
using Zenject;
using GameGuruCaseTwo.Systems.EventSystem;
using GameGuruCaseTwo.Entities.SliceSystem;
using GameGuruCaseTwo.Entities.Platform;
using GameGuruCaseTwo.Systems.GameSystem;

namespace GameGuruCaseTwo.ZenjectInstallers
{
    public class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        [SerializeField] private PlayReferences _playReferences;


        public override void InstallBindings()
        {
            Container.BindInstance(_playReferences);

            Container.Bind<EventManager>().AsSingle();
            Container.Bind<PlatformSegmentSlicer>().AsSingle();
            Container.Bind<PlatformHandler>().AsSingle();
            Container.Bind<GameStateController>().AsSingle();
        }
    }
}