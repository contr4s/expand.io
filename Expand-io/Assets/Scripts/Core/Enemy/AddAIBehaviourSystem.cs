using System;
using System.Collections.Generic;
using Util.Factory;
using Random = UnityEngine.Random;

namespace Core.Enemy
{
    using Scellecs.Morpeh;

    public sealed class AddAIBehaviourSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _filter;

        private readonly List<IBehaviourInfo> _behaviourInfos;
        
        public AddAIBehaviourSystem(IEnumerable<IBehaviourInfo> behaviourInfos)
        {
            _behaviourInfos = new List<IBehaviourInfo>(behaviourInfos);
        }

        public void OnAwake()
        {
            _filter = World.Filter.With<ControlledByAI>().Without<CurrentBehaviourStrategy>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                IBehaviourInfo behaviourInfo = null;
                if (entity.Has<ChangeBehaviourRequest>())
                {
                    Type newBehaviourType = entity.GetComponent<ChangeBehaviourRequest>().NewBehaviourType;
                    entity.RemoveComponent<ChangeBehaviourRequest>();
                    if (!TryGetBehaviour(newBehaviourType, out behaviourInfo))
                    {
                        continue;
                    }
                }
                
                behaviourInfo ??= GetBehaviour(entity);
                entity.SetComponent(new CurrentBehaviourStrategy{timeLeft = behaviourInfo.FollowTime});
                behaviourInfo.AddBehaviourComponent(entity);
            }
        }
        
        public void Dispose() { }
        
        private IBehaviourInfo GetBehaviour(Entity entity) => _behaviourInfos[Random.Range(0, _behaviourInfos.Count)];

        private bool TryGetBehaviour(Type componentType, out IBehaviourInfo behaviourInfo)
        {
            foreach (IBehaviourInfo info in _behaviourInfos) {
                if (info.BehaviourType == componentType) {
                    behaviourInfo = info;
                    return true;
                }
            }
            
            behaviourInfo = null;
            return false;
        }
        
        public class Factory : IFactory<AddAIBehaviourSystem>
        {
            private readonly IEnumerable<IBehaviourInfo> _behaviourInfos;
            
            public Factory(IEnumerable<IBehaviourInfo> behaviourInfos) => _behaviourInfos = behaviourInfos;
            
            public AddAIBehaviourSystem Create() => new AddAIBehaviourSystem(_behaviourInfos);
        }
    }
}