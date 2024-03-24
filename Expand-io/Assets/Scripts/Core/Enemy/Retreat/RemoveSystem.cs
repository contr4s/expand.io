using Core.Enemy.Util;
using Core.Expansion;
using Core.Movement;
using Scellecs.Morpeh;
using UnityEngine;
using Util.Factory;

namespace Core.Enemy.Retreat
{
    public class RemoveSystem : RemoveBehaviourSystem<Retreat>
    {
        private Filter _othersFilter;

        private readonly RetreatBehaviourInfo _behaviourInfo;

        public RemoveSystem(RetreatBehaviourInfo behaviourInfo)
        {
            _behaviourInfo = behaviourInfo;
        }

        public override void OnAwake()
        {
            base.OnAwake();
            _othersFilter = World.Filter.With<Consumer>().With<Size>().With<Place>().Build();
        }

        protected override bool CanApply(Entity entity)
        {
            float size = entity.GetComponent<Size>().size;
            foreach (Entity other in _othersFilter)
            {
                float otherSize = other.GetComponent<Size>().size;
                if (other.ID.Equals(entity.ID) || otherSize < size)
                {
                    continue;
                }

                float triggeringRadius = _behaviourInfo.GetRetreatDistance(otherSize, false);
                Vector2 position = entity.GetComponent<Place>().position;
                Vector2 otherPosition = other.GetComponent<Place>().position;
                if ((position - otherPosition).sqrMagnitude < triggeringRadius * triggeringRadius)
                {
                    return true;
                }
            }

            return false;
        }

        public class Factory : IFactory<RemoveSystem>
        {
            private readonly RetreatBehaviourInfo _behaviourInfo;

            public Factory(RetreatBehaviourInfo behaviourInfo) => _behaviourInfo = behaviourInfo;

            public RemoveSystem Create() => new(_behaviourInfo);
        }
    }
}