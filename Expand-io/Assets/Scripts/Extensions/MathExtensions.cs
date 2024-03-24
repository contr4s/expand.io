using UnityEngine;

namespace Extensions
{
    public static class MathExtensions
    {
        public static float ReRange(this float val,
                                    float oldStart,
                                    float oldEnd,
                                    float newStart,
                                    float newEnd) => Mathf.Lerp(newStart, newEnd, Mathf.InverseLerp(oldStart, oldEnd, val));
        
        public static float ReRange01(this float val, float oldStart, float oldEnd) => val.ReRange(oldStart, oldEnd, 0, 1);
    }
}