using Core.EntityView;
using UnityEngine;
using Util.Creation;

namespace Core.Expansion
{
    [CreateAssetMenu(fileName = "FoodConfig", menuName = "Configs/Food")]
    public class FoodConfig : ScriptableObject, ICreatableObjectConfig
    {
        [field: SerializeField] public SpriteRendererView ViewPrefab { get; private set; }
        [field: SerializeField] public float Size { get; private set; }
        [field: SerializeField] public float NutritionCoef { get; private set; }
        [field: SerializeField] public int StartCount { get; private set; }
        [field: SerializeField] public float SpawnInterval { get; private set; }
    }
}