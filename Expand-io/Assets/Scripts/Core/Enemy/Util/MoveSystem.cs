using Core.Movement;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Enemy.Util
{
    public abstract class MoveSystem<T> : ISystem where T : struct, IComponent
    {
        public World World { get; set; }

        private Filter _filter;
        private Filter _othersFilter;

        public void OnAwake()
        {
            _filter = World.Filter.With<T>().With<MoveDirection>().Build();
            _othersFilter = BuildOthersFilter();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_othersFilter.IsEmpty())
            {
                return;
            }

            foreach (Entity entity in _filter)
            {
                Vector2 closestDirection = Vector2.positiveInfinity;
                bool any = false;
                foreach (Entity other in _othersFilter)
                {
                    if (!IsInterestIn(entity, other))
                    {
                        continue;
                    }

                    Vector2 diff = other.GetComponent<Place>().position - entity.GetComponent<Place>().position;
                    if (diff.sqrMagnitude < closestDirection.sqrMagnitude)
                    {
                        any = true;
                        closestDirection = diff;
                    }
                }

                if (!any)
                {
                    continue;
                }

                var targetDirection = GetDirection(closestDirection);
                ref MoveDirection moveDirection = ref entity.GetComponent<MoveDirection>();
                Vector2 prevDir = moveDirection.direction;
                if (prevDir == Vector2.zero)
                {
                    moveDirection.direction = targetDirection.normalized;
                    continue;
                }
                
                float dotProduct = Vector2.Dot(prevDir, targetDirection);
                float sqrAngleCos = dotProduct * dotProduct / prevDir.sqrMagnitude / targetDirection.sqrMagnitude;
                moveDirection.direction = Vector2.Lerp(prevDir, targetDirection, deltaTime) * sqrAngleCos;
            }
        }

        protected virtual Vector2 GetDirection(Vector2 closestDirection) => closestDirection;

        protected abstract bool IsInterestIn(Entity entity, Entity other);
        protected abstract Filter BuildOthersFilter();

        public void Dispose() { }
    }
}