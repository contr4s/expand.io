using Core.Enemy.Util;
using Core.Expansion;
using Core.Movement;
using Scellecs.Morpeh;
using UnityEngine;
using Util.Factory;

namespace Core.Enemy.MoveToNearestStaticFood
{
    public class MoveSystem : MoveSystem<MoveToNearestStaticFood>
    {

        protected override bool IsInterestIn(Entity entity, Entity other) => true;

        protected override Filter BuildOthersFilter() => World.Filter.With<Food>().With<Place>().Without<MoveDirection>().Build();

        public class Factory : TemplateFactory<MoveSystem> { }
    }
}