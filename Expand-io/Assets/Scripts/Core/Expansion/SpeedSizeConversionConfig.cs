using UnityEngine;

namespace Core.Expansion
{
    [CreateAssetMenu(fileName = "SpeedSizeConversionConfig", menuName = "Configs/SpeedSize")]
    public class SpeedSizeConversionConfig : ScriptableObject
    {
        [SerializeField] private float _speedSizeConversion;
        [SerializeField] private float _maxSizeConversion;
        [SerializeField] private AnimationCurve _speedSizeConversionCurve;

        public float GetSpeed(float size) => _speedSizeConversion *
                                             _speedSizeConversionCurve.Evaluate(
                                                     Mathf.Lerp(0, 1, Mathf.InverseLerp(_maxSizeConversion, 1, size)));
    }
}