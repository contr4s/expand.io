using Core.Enemy.Util;
using Core.Expansion;
using Core.Movement;
using Scellecs.Morpeh;
using UnityEngine;
using Util.Factory;

namespace Core.Enemy.MoveToAnotherConsumer
{
    public class RemoveSystem : RemoveBehaviourSystem<MoveToAnotherConsumer>
    {
        private Filter _othersFilter;

        private readonly MoveToAnotherConsumerBehaviourInfo _behaviourInfo;

        public RemoveSystem(MoveToAnotherConsumerBehaviourInfo behaviourInfo)
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
            float triggeringRadius = _behaviourInfo.GetTriggeringRadius(size);
            foreach (Entity other in _othersFilter)
            {
                if (other.ID.Equals(entity.ID) || other.GetComponent<Size>().size > size)
                {
                    continue;
                }

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
            private readonly MoveToAnotherConsumerBehaviourInfo _behaviourInfo;

            public Factory(MoveToAnotherConsumerBehaviourInfo behaviourInfo) => _behaviourInfo = behaviourInfo;

            public RemoveSystem Create() => new(_behaviourInfo);
        }
    }
}