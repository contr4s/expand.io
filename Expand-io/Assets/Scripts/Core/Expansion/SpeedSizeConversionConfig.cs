using Core.Common;
using Extensions;
using UnityEngine;

namespace Core.Expansion
{
    [CreateAssetMenu(fileName = "SpeedSizeConversionConfig", menuName = "Configs/SpeedSize")]
    public class SpeedSizeConversionConfig : ScriptableObject
    {
        [SerializeField] private float _speedSizeConversion;
        [SerializeField] private ConstantsConfig _constantsConfig;
        [SerializeField] private AnimationCurve _speedSizeConversionCurve;

        public float GetSpeed(float size) => _speedSizeConversion *
                                             _speedSizeConversionCurve.Evaluate(
                                                     size.ReRange01(_constantsConfig.MaxSize, 1));
    }
}