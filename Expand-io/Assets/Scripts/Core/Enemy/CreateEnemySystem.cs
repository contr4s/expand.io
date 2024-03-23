using Core.Common;
using Core.EntityView;
using Core.Expansion;
using Core.Movement;
using Scellecs.Morpeh;
using UnityEngine;
using Util.Creation;
using Util.ObjectPool;

namespace Core.Enemy
{
    public sealed class CreateEnemySystem : CreateSystem<EnemyConfig>
    {
        public CreateEnemySystem(Map map, EnemyConfig config, IPoolableObjectProvider poolableObjectProvider) 
                : base(map, config, poolableObjectProvider) { }
        
        protected override void SetupEntity(Entity entity)
        {
            entity.AddComponent<MoveDirection>();
            entity.AddComponent<MoveSpeed>();
            entity.AddComponent<Consumer>();
            
            entity.SetComponent(new Size {size = Config.StartSize});
            entity.SetComponent(new Food {nutritionCoef = Config.NutritionCoef});
            entity.SetComponent(new ViewConfig {viewPrefab = Config.ViewPrefab, color = Random.ColorHSV()});
            entity.SetComponent(new ControlledByAI {aggressiveness = Random.Range(0, 1)});
        }

        public class Factory : Factory<CreateEnemySystem>
        {
            public Factory(Map map, EnemyConfig config, IPoolableObjectProvider poolableObjectProvider) 
                    : base(map, config, poolableObjectProvider) { }

            protected override CreateEnemySystem Create(Map map,
                                                        EnemyConfig config,
                                                        IPoolableObjectProvider poolableObjectProvider) =>
                    new CreateEnemySystem(map, config, poolableObjectProvider);
        }
    }
}