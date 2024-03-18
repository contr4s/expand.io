namespace Util.Factory
{
    public class TemplateFactory<T> : IFactory<T> where T : new()
    {
        public T Create()
        {
            return new T();
        }
    }
}