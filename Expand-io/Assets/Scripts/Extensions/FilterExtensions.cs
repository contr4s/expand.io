using Core.Expansion;
using Core.Movement;
using Scellecs.Morpeh;
using UnityEngine;

namespace Extensions
{
    public static class FilterExtensions
    {
        public static bool IsPointAvailable(this Filter filter, Vector2 point)
        {
            foreach (Entity entity in filter)
            {
                Vector2 position = entity.GetComponent<Place>().position;
                float size = entity.GetComponent<Size>().size;
                if (position.IsPointInsideCircle(point, size))
                {
                    return false;
                }
            }

            return true;
        }
    }
}