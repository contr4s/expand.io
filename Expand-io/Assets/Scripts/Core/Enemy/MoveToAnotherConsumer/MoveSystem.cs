using Core.Enemy.Util;
using Core.Expansion;
using Core.Movement;
using Scellecs.Morpeh;
using Util.Factory;

namespace Core.Enemy.MoveToAnotherConsumer
{
    public class MoveSystem : MoveSystem<MoveToAnotherConsumer>
    {
        protected override bool IsInterestIn(Entity entity, Entity other) =>
                !other.ID.Equals(entity.ID) && other.GetComponent<Size>().size < entity.GetComponent<Size>().size;

        protected override Filter BuildOthersFilter() =>
                World.Filter.With<Consumer>().With<Size>().With<Place>().Build();

        public class Factory : TemplateFactory<MoveSystem> { }
    }
}