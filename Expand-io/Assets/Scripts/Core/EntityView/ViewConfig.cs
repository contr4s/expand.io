using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.EntityView
{
    [Serializable]
    public struct ViewConfig : IComponent
    {
        public SpriteRendererView viewPrefab;
        public Color color;
    }
}