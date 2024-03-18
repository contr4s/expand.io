using Configs;
using Core.Movement;
using Scellecs.Morpeh;
using Util.Factory;

namespace Core.Expansion
{
    public sealed class AlignSpeedWithSizeSystem : ISystem
    {
        public World World { get; set; }

        private Filter _filter;
        
        private readonly ConstantsConfig _constantsConfig;
        
        public AlignSpeedWithSizeSystem(ConstantsConfig constantsConfig)
        {
            _constantsConfig = constantsConfig;
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
                speed.speed = _constantsConfig.SpeedSizeConversion / size;
            }
        }
        
        public void Dispose() { }
        
        public class Factory : IFactory<AlignSpeedWithSizeSystem>
        {
            private readonly ConstantsConfig _constantsConfig;
            
            public Factory(ConstantsConfig constantsConfig)
            {
                _constantsConfig = constantsConfig;
            }

            public AlignSpeedWithSizeSystem Create() => new AlignSpeedWithSizeSystem(_constantsConfig);
        }
    }
}