using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Enemy.Util
{
    public abstract class BehaviourInfo<T> : ScriptableObject, IBehaviourInfo where T : struct, IComponent
    {
        [field: SerializeField] public float Aggressiveness { get; private set; }
        [field: SerializeField] public float FollowTime { get; private set; }
        public Type BehaviourType => typeof(T);

        public void AddBehaviourComponent(Entity entity) => entity.AddComponent<T>();
    }
}