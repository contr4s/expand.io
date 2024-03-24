using Core.Common;
using Extensions;
using UnityEngine;

namespace Core.Player
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "Configs/Camera")]
    public class CameraConfig : ScriptableObject
    {
        [field: SerializeField] public float CameraSmoothTime { get; private set; }
        
        [SerializeField] private float _sizeMultiplier;
        [SerializeField] private ConstantsConfig _constantsConfig;
        [SerializeField] private AnimationCurve _triggeringRadiusCurve;

        public float GetCameraSize(float size) => _sizeMultiplier *
                                                        _triggeringRadiusCurve.Evaluate(
                                                                size.ReRange01(1, _constantsConfig.MaxSize));
    }
}