using Core.Common;
using Core.Expansion;
using Core.Movement;
using Extensions;
using Scellecs.Morpeh;
using UnityEngine;
using Util.Factory;
using Util.ObjectPool;

namespace Util.Creation
{
    public abstract class CreateSystem<T> : ISystem where T : ICreatableObjectConfig
    {
        public World World { get; set; }

        private Filter _filter;
        private float _lastSpawnTime;

        private readonly T _config;
        private readonly IPoolableObjectProvider _poolableObjectProvider;
        private readonly Map _map;

        protected T Config => _config;

        protected CreateSystem(Map map, T config, IPoolableObjectProvider poolableObjectProvider)
        {
            _map = map;
            _config = config;
            _poolableObjectProvider = poolableObjectProvider;
        }

        public void OnAwake()
        {
            _filter = World.Filter.With<Place>().With<Food>().Build();

            _poolableObjectProvider.RegisterPoolable(_config.ViewPrefab, _config.StartCount * 2);
            for (int i = 0; i < _config.StartCount; i++)
            {
                TrySpawnFood();
            }
        }

        public void OnUpdate(float deltaTime)
        {
            if (Time.time - _lastSpawnTime < _config.SpawnInterval)
            {
                return;
            }

            if (TrySpawnFood())
            {
                _lastSpawnTime = Time.time;
            }
        }

        public void Dispose() { }

        protected abstract void SetupEntity(Entity entity);

        private bool TrySpawnFood()
        {
            Vector2 point = _map.RandomPointInside;
            if (!_filter.IsPointAvailable(point))
            {
                return false;
            }

            Entity entity = World.CreateEntity();
            entity.SetComponent(new Place {position = point});
            SetupEntity(entity);

            return true;
        }

        public abstract class Factory<TSystem> : IFactory<TSystem> where TSystem : CreateSystem<T>
        {
            private readonly Map _map;
            private readonly T _config;
            private readonly IPoolableObjectProvider _poolableObjectProvider;

            protected Factory(Map map, T config, IPoolableObjectProvider poolableObjectProvider)
            {
                _map = map;
                _config = config;
                _poolableObjectProvider = poolableObjectProvider;
            }

            public TSystem Create() => Create(_map, _config, _poolableObjectProvider);

            protected abstract TSystem Create(Map map, T config, IPoolableObjectProvider poolableObjectProvider);
        }
    }
}