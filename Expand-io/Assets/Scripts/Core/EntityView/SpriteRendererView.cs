using UnityEngine;
using Util.ObjectPool;

namespace Core.EntityView
{
    public class SpriteRendererView : MonoBehaviour, IPoolable
    {
        public SpriteRenderer SpriteRenderer;
    }
}