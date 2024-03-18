using UnityEngine;
using Zenject;

namespace Configs
{
    [CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Configs/ConfigsInstaller")]
    public class ConfigsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private PlayerConfig playerConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(playerConfig).AsSingle();
        }
    }
}