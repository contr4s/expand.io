using System;
using Scellecs.Morpeh;

namespace Core.Enemy
{
    [Serializable]
    public struct ChangeBehaviourRequest : IComponent
    {
        public Type NewBehaviourType;
    }
}