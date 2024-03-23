using Core.Common;
using Extensions;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using Zenject;
using IFactory = Util.Factory.IFactory<Scellecs.Morpeh.IInitializer>;

namespace Core.Startup
{
    public class StartupInstaller : MonoInstaller
    {
        [SerializeField] private WorldViewer _worldViewer;
        [SerializeField] private float _mapWidth;
        [SerializeField] private float _mapHeight;
        [SerializeField] private SpriteRenderer _mapBackground;
        [SerializeField] private float cellSize;
        
        public override void InstallBindings()
        {
            Map map = new Map(_mapWidth, _mapHeight, _mapBackground, cellSize);
            Container.BindInstance(map).AsSingle();
            Container.BindInstance(_worldViewer).AsSingle();
            Container.BindAllImplementationsOfType<IFactory>();
            Container.BindInterfacesAndSelfTo<GameStartup>().AsSingle();
        }
    }
}