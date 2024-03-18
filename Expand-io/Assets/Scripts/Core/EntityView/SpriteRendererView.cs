using ObjectPool;
using UnityEngine;

namespace Core.EntityView
{
    public class SpriteRendererView : MonoBehaviour, IPoolable
    {
        public SpriteRenderer SpriteRenderer;
    }
}