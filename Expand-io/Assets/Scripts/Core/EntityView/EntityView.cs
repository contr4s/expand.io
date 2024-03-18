using System;
using Scellecs.Morpeh;

namespace Core.EntityView
{
    [Serializable]
    public struct EntityView : IComponent, IDisposable
    {
        public SpriteRendererView view;
        public Action<SpriteRendererView> ReturnToPoolAction;
        
        public void Dispose()
        {
            ReturnToPoolAction?.Invoke(view);
        }
    }
}