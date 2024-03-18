using Extensions;
using UnityEngine;
using Zenject;
using Installer = Scellecs.Morpeh.Installer;
using IFactory = Util.Factory.IFactory<Scellecs.Morpeh.IInitializer>;

namespace Core.Startup
{
    public class StartupInstaller : MonoInstaller
    {
        [SerializeField] private Installer _installer;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_installer).AsSingle();
            Container.BindAllImplementationsOfType<IFactory>();
            Container.BindInterfacesAndSelfTo<GameStartup>().AsSingle();
        }
    }
}