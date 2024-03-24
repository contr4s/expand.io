using Core.Common;
using Core.Enemy.Util;
using Extensions;
using UnityEngine;

namespace Core.Enemy.Retreat
{
    [CreateAssetMenu(fileName = "RetreatBehaviourInfo", menuName = "Configs/Behaviour/Retreat")]
    public class RetreatBehaviourInfo : BehaviourInfo<Retreat>
    {
        [SerializeField] private ConstantsConfig _constantsConfig;
        [SerializeField] private AnimationCurve _triggeringRadiusCurve;
        [SerializeField] private float _defaultRetreatDistance;
        [SerializeField] private float _forceRetreatDistance;

        public float GetRetreatDistance(float size, bool isForceRetreat) => size +
                                                                            GetTriggeringRadiusMultiplier(size) *
                                                                            (isForceRetreat
                                                                                     ? _forceRetreatDistance
                                                                                     : _defaultRetreatDistance);

        private float GetTriggeringRadiusMultiplier(float size) =>
                _triggeringRadiusCurve.Evaluate(size.ReRange01(1, _constantsConfig.MaxSize));
    }
}