using Core.Movement;
using Scellecs.Morpeh;
using Util.Factory;

namespace Core.Expansion
{
    public sealed class AlignSpeedWithSizeSystem : ISystem
    {
        public World World { get; set; }

        private Filter _filter;

        private readonly SpeedSizeConversionConfig _speedSizeConversionConfig;

        public AlignSpeedWithSizeSystem(SpeedSizeConversionConfig speedSizeConversionConfig)
        {
            _speedSizeConversionConfig = speedSizeConversionConfig;
        }

        public void OnAwake()
        {
            _filter = World.Filter.With<Size>().With<MoveSpeed>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                float size = entity.GetComponent<Size>().size;
                ref MoveSpeed speed = ref entity.GetComponent<MoveSpeed>();
                speed.speed = _speedSizeConversionConfig.GetSpeed(size);
            }
        }

        public void Dispose() { }

        public class Factory : IFactory<AlignSpeedWithSizeSystem>
        {
            private readonly SpeedSizeConversionConfig _speedSizeConversionConfig;

            public Factory(SpeedSizeConversionConfig speedSizeConversionConfig) =>
                    _speedSizeConversionConfig = speedSizeConversionConfig;

            public AlignSpeedWithSizeSystem Create() => new AlignSpeedWithSizeSystem(_speedSizeConversionConfig);
        }
    }
}