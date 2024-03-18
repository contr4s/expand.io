using Core.Expansion;
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
        private float _startOrthographicSize;
        private float _targetOrthographicSize;
        
        private Filter _filter;

        public void OnAwake()
        {
            _camera = Camera.main;
            _startOrthographicSize = _camera.orthographicSize;
            _filter = World.Filter.With<Player>().With<Place>().With<Size>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _targetOrthographicSize, deltaTime);
            
            foreach (Entity entity in _filter)
            {
                Vector2 position = entity.GetComponent<Place>().position;
                Transform transform = _camera.transform;
                transform.position = new Vector3(position.x, position.y, transform.position.z);

                float size = entity.GetComponent<Size>().size;
                _targetOrthographicSize = size * _startOrthographicSize;
            }
        }
        
        public void Dispose() { }
        
        public class Factory : TemplateFactory<AlignCameraSystem> { }
    }
}