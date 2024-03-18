using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Movement
{
    [Serializable]
    public struct MoveDirection : IComponent
    {
        public Vector2 direction;
    }
}