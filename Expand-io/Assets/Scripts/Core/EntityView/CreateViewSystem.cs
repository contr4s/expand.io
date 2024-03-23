using Util.Factory;
using Util.ObjectPool;

namespace Core.EntityView
{
    using Scellecs.Morpeh;

    public sealed class CreateViewSystem : ISystem
    {
        public World World { get; set; }

        private Filter _filter;

        private readonly IPoolableObjectProvider _poolableObjectProvider;

        public CreateViewSystem(IPoolableObjectProvider poolableObjectProvider)
        {
            _poolableObjectProvider = poolableObjectProvider;
        }

        public void OnAwake()
        {
            _filter = World.Filter.Without<EntityView>().With<ViewConfig>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                ViewConfig viewConfig = entity.GetComponent<ViewConfig>();
                SpriteRendererView spriteRendererView = _poolableObjectProvider.GetFromPool(viewConfig.viewPrefab);
                spriteRendererView.SpriteRenderer.color = viewConfig.color;
                entity.SetComponent(new EntityView {view = spriteRendererView});
            }
        }

        public void Dispose() { }

        public class Factory : IFactory<CreateViewSystem>
        {
            private readonly IPoolableObjectProvider _poolableObjectProvider;

            public Factory(IPoolableObjectProvider poolableObjectProvider)
            {
                _poolableObjectProvider = poolableObjectProvider;
            }

            public CreateViewSystem Create() => new(_poolableObjectProvider);
        }
    }
}