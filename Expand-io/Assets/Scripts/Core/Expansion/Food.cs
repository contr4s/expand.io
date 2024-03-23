using System;
using Scellecs.Morpeh;

namespace Core.Expansion
{
    [Serializable]
    public struct Food : IComponent
    {
        public float nutritionCoef;
    }
}