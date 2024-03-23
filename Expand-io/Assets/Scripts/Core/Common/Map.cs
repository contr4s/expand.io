using UnityEngine;

namespace Core.Common
{
    public class Map
    {
        private readonly float _xMax;
        private readonly float _yMax;
        private readonly float _xMin;
        private readonly float _yMin;

        public Map(float xMax,
                   float yMax,
                   float xMin,
                   float yMin,
                   SpriteRenderer mapBackground,
                   float cellSize)
        {
            _xMax = xMax;
            _yMax = yMax;
            _xMin = xMin;
            _yMin = yMin;
            mapBackground.size = new Vector2((_xMax - _xMin) / cellSize, (_yMax - _yMin) / cellSize);
            mapBackground.transform.localScale = new Vector3(cellSize, cellSize, 1);
        }

        public Map(float width, float height, SpriteRenderer mapBackground,
                   float cellSize) : this(
                width / 2, height / 2, -width / 2, -height / 2, mapBackground, cellSize) { }

        public Vector2 RandomPointInside => new Vector2(Random.Range(_xMin, _xMax), Random.Range(_yMin, _yMax));

        public bool IsInside(Vector2 position) =>
                position.x <= _xMax && position.x >= _xMin && position.y <= _yMax && position.y >= _yMin;

        public Vector2 ClampPosition(Vector2 position) => IsInside(position)
                                                                  ? position
                                                                  : new Vector2(
                                                                          Mathf.Clamp(position.x, _xMin, _xMax),
                                                                          Mathf.Clamp(position.y, _yMin, _yMax));
    }
}