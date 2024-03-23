using Core.Common;
using Core.EntityView;
using UnityEngine;
using Util.Creation;
using Util.ObjectPool;

namespace Core.Expansion
{
    using Scellecs.Morpeh;

    public sealed class CreateFoodSystem : CreateSystem<FoodConfig>
    {
        public CreateFoodSystem(Map map, FoodConfig config, IPoolableObjectProvider poolableObjectProvider)
                : base(map, config, poolableObjectProvider) { }

        protected override void SetupEntity(Entity entity)
        {
            entity.SetComponent(new Size {size = Config.Size});
            entity.SetComponent(new Food {nutritionCoef = Config.NutritionCoef});
            entity.SetComponent(new ViewConfig {viewPrefab = Config.ViewPrefab, color = Random.ColorHSV()});
        }

        public class Factory : Factory<CreateFoodSystem>
        {
            public Factory(Map map, FoodConfig config, IPoolableObjectProvider poolableObjectProvider) 
                    : base(map, config, poolableObjectProvider) { }

            protected override CreateFoodSystem Create(Map map,
                                                       FoodConfig config,
                                                       IPoolableObjectProvider poolableObjectProvider) =>
                    new CreateFoodSystem(map, config, poolableObjectProvider);
        }
    }
}