using Core.EntityView;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "FoodConfig", menuName = "Configs/Food")]
    public class FoodConfig : ScriptableObject
    {
        [field: SerializeField] public SpriteRendererView ViewPrefab { get; private set; }
        [field: SerializeField] public float Size { get; private set; }
        [field: SerializeField] public int StartCount { get; private set; }
        [field: SerializeField] public float SpawnInterval { get; private set; }
    }
}