using Core.Enemy.Util;
using UnityEngine;

namespace Core.Enemy.MoveToAnotherConsumer
{
    [CreateAssetMenu(fileName = "MoveToAnotherConsumerBehaviourInfo", menuName = "Configs/Behaviour/MoveToAnotherConsumer")]
    public class MoveToAnotherConsumerBehaviourInfo : BehaviourInfo<MoveToAnotherConsumer>
    {
        [SerializeField] private float _radiusMultiplier;
        [SerializeField] private float _maxSizeConversion;
        [SerializeField] private AnimationCurve _triggeringRadiusCurve;

        public float GetTriggeringRadius(float size) => _radiusMultiplier *
                                                        _triggeringRadiusCurve.Evaluate(
                                                                Mathf.Lerp(0, 1, Mathf.InverseLerp(1, _maxSizeConversion, size)));
    }
}