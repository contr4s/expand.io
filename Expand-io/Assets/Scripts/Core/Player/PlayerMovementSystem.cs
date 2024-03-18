using Core.Movement;
using UnityEngine;
using Util.Factory;

namespace Core.Player
{
    using Scellecs.Morpeh;

    public sealed class PlayerMovementSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _filter;
        private Camera _camera;

        public void OnAwake()
        {
            _camera = Camera.main;
            _filter = World.Filter.With<Player>().With<MoveDirection>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                Vector3 input = Input.mousePosition;
                Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Vector2 dir = (Vector2)input - screenCenter;
                ref MoveDirection moveDirection = ref entity.GetComponent<MoveDirection>();
                moveDirection.direction = new Vector2(dir.x / Screen.width, dir.y / Screen.height);
                Debug.Log(new Vector2(2 * dir.x / Screen.width, 2 * dir.y / Screen.height));

            }
        }
        
        public void Dispose() { }
        
        public class Factory : TemplateFactory<PlayerMovementSystem> { }
    }
}