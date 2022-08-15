using UnityEngine;
using Zenject;
using GameGuruCaseTwo.Systems.EventSystem;
using GameGuruCaseTwo.Entities.SliceSystem;
using GameGuruCaseTwo.Entities.Platform;
using GameGuruCaseTwo.Systems.GameSystem;
using GameGuruCaseTwo.Systems.AudioSystem;
using GameGuruCaseTwo.Systems.ComboSystem;
using GameGuruCaseTwo.Systems.LevelSystem;

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
            Container.Bind<AudioManager>().AsSingle();
            Container.Bind<SegmentComboManager>().AsSingle();
            Container.Bind<LevelFinisherSpawner>().AsSingle();
        }
    }
}