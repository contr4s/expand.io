using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ConstantsConfig", menuName = "Configs/Constants")]

    public class ConstantsConfig : ScriptableObject
    {
        [field: SerializeField] public float SpeedSizeConversion { get; private set; }
    }
}