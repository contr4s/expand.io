using Core.Common;
using Core.Enemy.Util;
using Extensions;
using UnityEngine;

namespace Core.Enemy.MoveToAnotherConsumer
{
    [CreateAssetMenu(fileName = "MoveToAnotherConsumerBehaviourInfo",
                     menuName = "Configs/Behaviour/MoveToAnotherConsumer")]
    public class MoveToAnotherConsumerBehaviourInfo : BehaviourInfo<MoveToAnotherConsumer>
    {
        [SerializeField] private float _radiusMultiplier;
        [SerializeField] private ConstantsConfig _constantsConfig;
        [SerializeField] private AnimationCurve _triggeringRadiusCurve;

        public float GetTriggeringRadius(float size) => size + _radiusMultiplier *
                                                        _triggeringRadiusCurve.Evaluate(
                                                                size.ReRange01(1, _constantsConfig.MaxSize));
    }
}