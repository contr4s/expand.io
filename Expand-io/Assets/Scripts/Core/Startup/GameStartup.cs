using System;
using System.Collections.Generic;
using Scellecs.Morpeh;
using Zenject;
using Installer = Scellecs.Morpeh.Installer;
using IFactory = Util.Factory.IFactory<Scellecs.Morpeh.IInitializer>;


namespace Core.Startup
{
    public class GameStartup : IInitializable, IDisposable
    {
        private readonly List<IFactory> _factories;
        private readonly Installer _installer;
        
        private World _world = World.Default;

        public GameStartup(IEnumerable<IFactory> factories, Installer installer)
        {
            _installer = installer;
            _factories = new List<IFactory>(factories);
        }

        public void StartGame()
        {
            _world = World.Create();
            SystemsGroup systemsGroup = _world.CreateSystemsGroup();
            _installer.World = _world;
            
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