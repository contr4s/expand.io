using Core.Movement;
using Util;
using Util.Factory;

namespace Core.EntityView
{
    using Scellecs.Morpeh;

    public sealed class AlignViewPositionSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _filter;

        public void OnAwake()
        {
            _filter = World.Filter.With<EntityView>().With<Place>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter) {
                if (entity.IsNullOrDisposed())
                {
                    continue;
                }
                
                Place place = entity.GetComponent<Place>();
                EntityView entityView = entity.GetComponent<EntityView>();
                entityView.view.transform.position = place.position;
            }
        }
        
        public void Dispose() { }
        
        public class Factory : TemplateFactory<AlignViewPositionSystem> { }
    }
}