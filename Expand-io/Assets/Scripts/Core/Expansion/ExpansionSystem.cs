using System;
using Core.EntityView;
using Core.Movement;
using Extensions;
using ObjectPool;
using UnityEngine;
using Util.Factory;

namespace Core.Expansion
{
    using Scellecs.Morpeh;

    public sealed class ExpansionSystem : ISystem
    {
        public World World { get; set; }

        private Filter _filter;

        private readonly IPoolableObjectProvider _poolableObjectProvider;

        public ExpansionSystem(IPoolableObjectProvider poolableObjectProvider)
        {
            _poolableObjectProvider = poolableObjectProvider;
        }

        public void OnAwake()
        {
            _filter = World.Filter.With<Size>().With<Place>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity first in _filter)
            {
                foreach (Entity second in _filter)
                {
                    if (first.IsNullOrDisposed() || second.IsNullOrDisposed() || first == second)
                    {
                        continue;
                    }
                    
                    Vector2 position1 = first.GetComponent<Place>().position;
                    Vector2 position2 = second.GetComponent<Place>().position;
                    ref Size size1 = ref first.GetComponent<Size>();
                    ref Size size2 = ref second.GetComponent<Size>();
                    float maxSize = Math.Max(size1.size, size2.size);

                    if (!position1.IsPointInsideCircle(position2, maxSize))
                    {
                        continue;
                    }

                    if (size1.size > size2.size)
                    {
                        size1.size += size2.size;
                        RemoveEntity(second);
                    }
                    else if (size2.size > size1.size)
                    {
                        size2.size += size1.size;
                        RemoveEntity(first);
                    }
                }
            }
        }

        public void Dispose() { }

        private void RemoveEntity(Entity entity)
        {
            if (entity.Has<EntityView.EntityView>())
            {
                SpriteRendererView spriteRendererView = entity.GetComponent<EntityView.EntityView>().view;
                _poolableObjectProvider.ReturnToPool(spriteRendererView);
            }

            World.RemoveEntity(entity);
        }

        public class Factory : IFactory<ExpansionSystem>
        {
            private readonly IPoolableObjectProvider _poolableObjectProvider;

            public Factory(IPoolableObjectProvider poolableObjectProvider) =>
                    _poolableObjectProvider = poolableObjectProvider;

            public ExpansionSystem Create() => new ExpansionSystem(_poolableObjectProvider);
        }
    }
}