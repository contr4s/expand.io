using Core.EntityView;
using UnityEngine;

namespace Core.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public SpriteRendererView PlayerPrefab { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public float StartSize { get; private set; }
        [field: SerializeField] public float NutritionCoef { get; private set; }
    }
}