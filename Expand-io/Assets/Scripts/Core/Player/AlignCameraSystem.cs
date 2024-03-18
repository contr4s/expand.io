using Core.Movement;
using UnityEngine;
using Util.Factory;

namespace Core.Player
{
    using Scellecs.Morpeh;

    public sealed class AlignCameraSystem : ILateSystem
    {
        public World World { get; set; }

        private Camera _camera;
        private Filter _filter;

        public void OnAwake()
        {
            _camera = Camera.main;
            _filter = World.Filter.With<Player>().With<Place>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                Vector2 position = entity.GetComponent<Place>().position;
                Transform transform = _camera.transform;
                transform.position = new Vector3(position.x, position.y, transform.position.z);
            }
        }
        
        public void Dispose() { }
        
        public class Factory : TemplateFactory<AlignCameraSystem> { }
    }
}