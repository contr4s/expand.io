using Core.Expansion;
using Core.Movement;
using UnityEngine;
using Util.Factory;

namespace Core.Enemy.Retreat
{
    using Scellecs.Morpeh;

    public sealed class ForceRetreatSystem : ISystem
    {
        public World World { get; set; }

        private Filter _filter;
        private Filter _othersFilter;

        private readonly RetreatBehaviourInfo _behaviourInfo;
        
        public ForceRetreatSystem(RetreatBehaviourInfo behaviourInfo) => _behaviourInfo = behaviourInfo;

        public void OnAwake()
        {
            _filter = World.Filter.With<CurrentBehaviourStrategy>().Build();
            _othersFilter = World.Filter.With<Consumer>().With<Size>().With<Place>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                float size = entity.GetComponent<Size>().size;
                foreach (Entity other in _othersFilter)
                {
                    float otherSize = other.GetComponent<Size>().size;
                    if (other.ID.Equals(entity.ID) || otherSize < size)
                    {
                        continue;
                    }

                    float triggeringRadius = _behaviourInfo.ForceRetreatDistance + otherSize;
                    Vector2 position = entity.GetComponent<Place>().position;
                    Vector2 otherPosition = other.GetComponent<Place>().position;
                    if ((position - otherPosition).sqrMagnitude > triggeringRadius * triggeringRadius)
                    {
                        continue;
                    }

                    ref CurrentBehaviourStrategy currentBehaviourStrategy
                            = ref entity.GetComponent<CurrentBehaviourStrategy>();

                    currentBehaviourStrategy.timeLeft = 0;
                    entity.SetComponent(new ChangeBehaviourRequest {NewBehaviourType = typeof(Retreat)});

                    return;
                }
            }
        }

        public void Dispose() { }

        public class Factory : IFactory<ForceRetreatSystem>
        {
            private readonly RetreatBehaviourInfo _behaviourInfo;
            public Factory(RetreatBehaviourInfo behaviourInfo) => _behaviourInfo = behaviourInfo;
            public ForceRetreatSystem Create() => new ForceRetreatSystem(_behaviourInfo);
        }
    }
}