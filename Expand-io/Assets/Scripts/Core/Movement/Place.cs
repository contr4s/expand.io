using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Movement
{
    [Serializable]
    public struct Place : IComponent
    {
        public Vector2 position;
    }
}