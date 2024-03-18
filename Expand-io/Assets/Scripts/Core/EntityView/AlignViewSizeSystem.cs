using Core.Expansion;
using UnityEngine;
using Util.Factory;

namespace Core.EntityView
{
    using Scellecs.Morpeh;

    public sealed class AlignViewSizeSystem : ISystem
    {
        public World World { get; set; }

        private Filter _filter;

        public void OnAwake()
        {
            _filter = World.Filter.With<EntityView>().With<Size>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                float size = entity.GetComponent<Size>().size;
                ref EntityView view = ref entity.GetComponent<EntityView>();
                view.view.transform.localScale = new Vector3(size * 2, size * 2, 1);
            }
        }

        public void Dispose() { }

        public class Factory : TemplateFactory<AlignViewSizeSystem> { }
    }
}