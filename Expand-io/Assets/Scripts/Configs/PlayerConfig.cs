using Core.EntityView;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public SpriteRendererView PlayerPrefab { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public float StartSize { get; private set; }
    }
}