using UnityEngine;
using Zenject;
using GameGuruCaseTwo.Datas.AssetData;
using GameGuruCaseTwo.Datas.GameData;

namespace GameGuruCaseTwo.ZenjectInstallers
{
    [CreateAssetMenu(fileName = "GameDataInstaller", menuName = "Installers/GameDataInstaller")]
    public class GameDataInstaller : ScriptableObjectInstaller<GameDataInstaller>
    {
        [SerializeField] private GameAssets _gameAssets;
        [SerializeField] private GameSettings _gameSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_gameAssets);
            Container.BindInstance(_gameSettings);
        }
    }
}