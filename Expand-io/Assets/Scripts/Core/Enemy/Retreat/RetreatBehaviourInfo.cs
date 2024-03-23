using Core.Enemy.Util;
using UnityEngine;

namespace Core.Enemy.Retreat
{
    [CreateAssetMenu(fileName = "RetreatBehaviourInfo", menuName = "Configs/Behaviour/Retreat")]
    public class RetreatBehaviourInfo : BehaviourInfo<Retreat>
    {
        [field: SerializeField] public float DefaultRetreatDistance { get; private set; }
        [field: SerializeField] public float ForceRetreatDistance { get; private set; }
    }
}