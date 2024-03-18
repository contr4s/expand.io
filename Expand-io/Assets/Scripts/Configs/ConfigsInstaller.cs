using UnityEngine;
using Zenject;

namespace Configs
{
    [CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Installers/ConfigsInstaller")]
    public class ConfigsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private ConstantsConfig constantsConfig;
        [SerializeField] private FoodConfig foodConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(playerConfig).AsSingle();
            Container.BindInstance(constantsConfig).AsSingle();
            Container.BindInstance(foodConfig).AsSingle();
        }
    }
}