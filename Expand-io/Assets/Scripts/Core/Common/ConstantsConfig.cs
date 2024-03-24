using UnityEngine;

namespace Core.Common
{
    [CreateAssetMenu(fileName = "ConstantsConfig", menuName = "Configs/Constants")]

    public class ConstantsConfig : ScriptableObject
    {
        [field: SerializeField] public int MaxSize { get; private set; }
    }
}