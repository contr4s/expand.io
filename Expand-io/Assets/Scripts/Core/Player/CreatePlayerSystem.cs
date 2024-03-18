using Configs;
using Core.EntityView;
using Core.Expansion;
using Core.Movement;
using Scellecs.Morpeh;
using Util.Factory;

namespace Core.Player
{
    public class CreatePlayerSystem : IInitializer
    {
        public World World { get; set; }

        private PlayerConfig _playerConfig;

        public CreatePlayerSystem(PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
        }

        public void OnAwake()
        {
            Entity entity = World.CreateEntity();
            entity.AddComponent<Player>();
            entity.AddComponent<Place>();
            entity.AddComponent<MoveDirection>();
            entity.AddComponent<MoveSpeed>();
            entity.SetComponent(new Size {size = _playerConfig.StartSize});
            entity.SetComponent(new ViewConfig {viewPrefab = _playerConfig.PlayerPrefab, color = _playerConfig.Color});
        }

        public void Dispose() { }

        public class Factory : IFactory<CreatePlayerSystem>
        {
            private PlayerConfig _playerConfig;

            public Factory(PlayerConfig playerConfig)
            {
                _playerConfig = playerConfig;
            }

            public CreatePlayerSystem Create() => new CreatePlayerSystem(_playerConfig);
        }
    }
}