using Core.EntityView;
using UnityEngine;
using Util.Creation;

namespace Core.Enemy
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject, ICreatableObjectConfig
    {
        [field: SerializeField] public SpriteRendererView ViewPrefab { get; private set; }
        [field: SerializeField] public float StartSize { get; private set; }
        [field: SerializeField] public int StartCount { get; private set; }
        [field: SerializeField] public float NutritionCoef { get; private set; }
        [field: SerializeField] public float SpawnInterval { get; private set; }
    }
}