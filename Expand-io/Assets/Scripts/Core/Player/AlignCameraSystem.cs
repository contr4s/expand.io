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
        private float _targetOrthographicSize;
        private Vector3 _targetPosition;
        private Vector3 _velocity;

        private Filter _filter;

        private readonly CameraConfig _cameraConfig;

        public AlignCameraSystem(CameraConfig cameraConfig)
        {
            _cameraConfig = cameraConfig;
        }

        public void OnAwake()
        {
            _camera = Camera.main;
            _targetPosition = _camera.transform.position;
            _filter = World.Filter.With<Player>().With<Place>().With<Size>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _targetOrthographicSize, deltaTime);
            _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, _targetPosition, ref _velocity,
                                                            _cameraConfig.CameraSmoothTime);

            foreach (Entity entity in _filter)
            {
                Vector2 position = entity.GetComponent<Place>().position;
                _targetPosition = new Vector3(position.x, position.y, _targetPosition.z);

                float size = entity.GetComponent<Size>().size;
                _targetOrthographicSize = _cameraConfig.GetCameraSize(size);
            }
        }

        public void Dispose() { }

        public class Factory : IFactory<AlignCameraSystem>
        {
            private readonly CameraConfig _cameraConfig;
            public Factory(CameraConfig cameraConfig) => _cameraConfig = cameraConfig;
            public AlignCameraSystem Create() => new AlignCameraSystem(_cameraConfig);
        }
    }
}