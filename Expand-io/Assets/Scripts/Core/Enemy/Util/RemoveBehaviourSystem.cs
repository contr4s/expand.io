using Scellecs.Morpeh;

namespace Core.Enemy.Util
{
    public abstract class RemoveBehaviourSystem<T> : ISystem where T : struct, IComponent
    {
        public World World { get; set; }
        
        private Filter _filter;

        public virtual void OnAwake()
        {
            _filter = World.Filter.With<T>().With<CurrentBehaviourStrategy>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                if (!CanApply(entity))
                {
                    entity.RemoveComponent<CurrentBehaviourStrategy>();
                    entity.RemoveComponent<T>();
                    continue;
                }
                
                ref CurrentBehaviourStrategy currentBehaviourStrategy = ref entity.GetComponent<CurrentBehaviourStrategy>();
                currentBehaviourStrategy.timeLeft -= deltaTime;
                if (currentBehaviourStrategy.timeLeft <= 0)
                {
                    entity.RemoveComponent<CurrentBehaviourStrategy>();
                    entity.RemoveComponent<T>();
                }
            }
        }

        protected abstract bool CanApply(Entity entity);
        
        public virtual void Dispose() { }
    }
}