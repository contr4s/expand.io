using Core.EntityView;

namespace Util.Creation
{
    public interface ICreatableObjectConfig
    {
        SpriteRendererView ViewPrefab { get; }
        int StartCount { get; }
        float SpawnInterval { get; }
    }
}