using UnityEngine;

namespace Extensions
{
    public static class Vector2Extensions
    {
        public static bool IsPointInsideCircle(this Vector2 point, Vector2 center, float radius)
        {
            return (center - point).sqrMagnitude <= radius * radius;
        }
    }
}