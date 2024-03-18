using Configs;
using Core.Common;
using Core.EntityView;
using Core.Movement;
using Extensions;
using UnityEngine;
using Util.Factory;

namespace Core.Expansion
{
    using Scellecs.Morpeh;

    public sealed class CreateFoodSystem : ISystem
    {
        public World World { get; set; }

        private Filter _filter;
        private float _lastSpawnTime;

        private readonly FoodConfig _foodConfig;
        private readonly Map _map;

        public CreateFoodSystem(Map map, FoodConfig foodConfig)
        {
            _map = map;
            _foodConfig = foodConfig;
        }

        public void OnAwake()
        {
            _filter = World.Filter.With<Place>().With<Size>().Build();

            for (int i = 0; i < _foodConfig.StartCount; i++)
            {
                TrySpawnFood();
            }
        }

        public void OnUpdate(float deltaTime)
        {
            if (Time.time - _lastSpawnTime < _foodConfig.SpawnInterval)
            {
                return;
            }

            if (TrySpawnFood())
            {
                _lastSpawnTime = Time.time;
            }
        }

        public void Dispose() { }

        private bool TrySpawnFood()
        {
            Vector2 point = _map.RandomPointInside;
            if (!_filter.IsPointAvailable(point))
            {
                return false;
            }

            Entity food = World.CreateEntity();
            food.SetComponent(new Place {position = point});
            food.SetComponent(new Size {size = _foodConfig.Size});
            food.SetComponent(new ViewConfig {viewPrefab = _foodConfig.ViewPrefab, color = Random.ColorHSV()});

            return true;
        }

        public class Factory : IFactory<CreateFoodSystem>
        {
            private readonly Map _map;
            private readonly FoodConfig _foodConfig;

            public Factory(Map map, FoodConfig foodConfig)
            {
                _map = map;
                _foodConfig = foodConfig;
            }

            public CreateFoodSystem Create() => new CreateFoodSystem(_map, _foodConfig);
        }
    }
}