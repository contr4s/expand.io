using System;
using Scellecs.Morpeh;

namespace Core.Enemy
{
    [Serializable]
    public struct CurrentBehaviourStrategy : IComponent
    {
        public float timeLeft;
    }
}