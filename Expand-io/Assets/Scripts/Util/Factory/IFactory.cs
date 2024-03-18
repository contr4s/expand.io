namespace Util.Factory
{
    public interface IFactory<out T>
    {
        T Create();
    }
}