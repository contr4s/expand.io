using Core.Common;
using UnityEngine;
using Util.Factory;

namespace Core.Movement
{
    using Scellecs.Morpeh;

    public sealed class MovementSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _filter;

        private readonly Map _map;
        
        public MovementSystem(Map map)
        
        {
            _map = map;
        }

        public void OnAwake()
        {
            _filter = World.Filter.With<MoveSpeed>().With<MoveDirection>().With<Place>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                ref Place position = ref entity.GetComponent<Place>();
                float speed = entity.GetComponent<MoveSpeed>().speed;
                Vector2 direction = entity.GetComponent<MoveDirection>().direction;
                position.position += direction * speed * deltaTime;
                position.position = _map.ClampPosition(position.position);
            }
        }
        
        public void Dispose() { }

        public class Factory : IFactory<MovementSystem>
        {
            private readonly Map _map;
            public Factory(Map map) => _map = map;

            public MovementSystem Create() => new(_map);
        }
        
    }
}