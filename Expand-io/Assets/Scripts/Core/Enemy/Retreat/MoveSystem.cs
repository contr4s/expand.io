using Core.Enemy.Util;
using Core.Expansion;
using Core.Movement;
using Scellecs.Morpeh;
using UnityEngine;
using Util.Factory;

namespace Core.Enemy.Retreat
{
    public class MoveSystem : MoveSystem<Retreat>
    {
        protected override bool IsInterestIn(Entity entity, Entity other) =>
                !other.ID.Equals(entity.ID) && other.GetComponent<Size>().size > entity.GetComponent<Size>().size;

        protected override Filter BuildOthersFilter() =>
                World.Filter.With<Consumer>().With<Size>().With<Place>().Build();

        protected override Vector2 GetDirection(Vector2 closestDirection) => -closestDirection;

        public class Factory : TemplateFactory<MoveSystem> { }
    }
}