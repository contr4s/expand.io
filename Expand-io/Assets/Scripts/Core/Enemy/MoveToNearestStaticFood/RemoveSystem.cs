using Core.Enemy.Util;
using Core.Expansion;
using Core.Movement;
using Scellecs.Morpeh;
using Util.Factory;

namespace Core.Enemy.MoveToNearestStaticFood
{
    public class RemoveSystem : RemoveBehaviourSystem<MoveToNearestStaticFood>
    {
        private Filter _othersFilter;

        public override void OnAwake()
        {
            base.OnAwake();
            _othersFilter = World.Filter.With<Food>().With<Size>().Without<MoveDirection>().Build();
        }

        protected override bool CanApply(Entity entity)
        {
            foreach (Entity other in _othersFilter)
            {
                if (other.GetComponent<Size>().size < entity.GetComponent<Size>().size)
                    return true;
            }

            return false;
        }

        public class Factory : TemplateFactory<RemoveSystem> { }
    }
}