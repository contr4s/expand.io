using UnityEngine;
using Util.Factory;

namespace Core.Movement
{
    using Scellecs.Morpeh;

    public sealed class MovementSystem : ISystem
    {
        public class Factory : TemplateFactory<MovementSystem> { }
        
        public World World { get; set; }
        
        private Filter _filter;

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
            }
        }
        
        public void Dispose() { }
    }
}