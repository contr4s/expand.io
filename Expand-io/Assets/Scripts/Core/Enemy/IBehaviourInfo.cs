using System;
using Scellecs.Morpeh;

namespace Core.Enemy
{
    public interface IBehaviourInfo
    {
        float Aggressiveness { get; }
        float FollowTime { get; }
        Type BehaviourType { get; }
        
        void AddBehaviourComponent(Entity entity);
    }
}