using System;
using Core.EntityView;
using Core.Movement;
using Extensions;
using UnityEngine;
using Util.Factory;
using Util.ObjectPool;

namespace Core.Expansion
{
    using Scellecs.Morpeh;

    public sealed class ExpansionSystem : ISystem
    {
        public World World { get; set; }

        private Filter _foodFilter;
        private Filter _consumerFilter;

        private readonly IPoolableObjectProvider _poolableObjectProvider;

        public ExpansionSystem(IPoolableObjectProvider poolableObjectProvider)
        {
            _poolableObjectProvider = poolableObjectProvider;
        }

        public void OnAwake()
        {
            _foodFilter = World.Filter.With<Food>().With<Size>().With<Place>().Build();
            _consumerFilter = World.Filter.With<Consumer>().With<Size>().With<Place>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity consumer in _consumerFilter)
            {
                foreach (Entity food in _foodFilter)
                {
                    if (consumer.IsNullOrDisposed() || food.IsNullOrDisposed() || consumer == food)
                    {
                        continue;
                    }

                    Vector2 consumerPos = consumer.GetComponent<Place>().position;
                    Vector2 foodPos = food.GetComponent<Place>().position;
                    ref Size consumerSize = ref consumer.GetComponent<Size>();
                    Size foodSize = food.GetComponent<Size>();

                    if (consumerSize.size < foodSize.size ||
                        !consumerPos.IsPointInsideCircle(foodPos, consumerSize.size))
                    {
                        continue;
                    }

                    consumerSize.size += foodSize.size * food.GetComponent<Food>().nutritionCoef;
                    if (food.Has<EntityView.EntityView>())
                    {
                        SpriteRendererView spriteRendererView = food.GetComponent<EntityView.EntityView>().view;
                        _poolableObjectProvider.ReturnToPool(spriteRendererView);
                    }
                    World.RemoveEntity(food);
                }
            }
        }

        public void Dispose() { }

        public class Factory : IFactory<ExpansionSystem>
        {
            private readonly IPoolableObjectProvider _poolableObjectProvider;

            public Factory(IPoolableObjectProvider poolableObjectProvider) =>
                    _poolableObjectProvider = poolableObjectProvider;

            public ExpansionSystem Create() => new ExpansionSystem(_poolableObjectProvider);
        }
    }
}