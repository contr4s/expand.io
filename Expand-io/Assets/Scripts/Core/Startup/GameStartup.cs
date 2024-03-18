using System;
using System.Collections.Generic;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Zenject;
using IFactory = Util.Factory.IFactory<Scellecs.Morpeh.IInitializer>;


namespace Core.Startup
{
    public class GameStartup : IInitializable, IDisposable
    {
        private readonly List<IFactory> _factories;
        private readonly WorldViewer _worldViewer;
        
        private World _world = World.Default;

        public GameStartup(IEnumerable<IFactory> factories, WorldViewer worldViewer)
        {
            _worldViewer = worldViewer;
            _factories = new List<IFactory>(factories);
        }

        public void StartGame()
        {
            _world = World.Create();
            SystemsGroup systemsGroup = _world.CreateSystemsGroup();
            _worldViewer.World = _world;
            
            foreach (var factory in _factories)
            {
                IInitializer initializer = factory.Create();
                if (initializer is ISystem system)
                {
                    systemsGroup.AddSystem(system);
                }
                else
                {
                    systemsGroup.AddInitializer(initializer);
                }
            }
            
            _world.AddSystemsGroup(0, systemsGroup);
            _world.UpdateByUnity = true;
        }

        public void FinishGame()
        {
            _world.Dispose();
        }

        public void Initialize()
        {
            StartGame();
        }

        public void Dispose()
        {
            _world?.Dispose();
        }
    }
}