using System;
using Scellecs.Morpeh;

namespace Core.Enemy
{
    [Serializable]
    public struct ControlledByAI : IComponent
    {
        public float aggressiveness;
    }
}