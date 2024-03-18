using System;
using Scellecs.Morpeh;

namespace Core.EntityView
{
    [Serializable]
    public struct EntityView : IComponent
    {
        public SpriteRendererView view;
    }
}