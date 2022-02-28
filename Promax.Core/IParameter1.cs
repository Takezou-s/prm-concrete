namespace Promax.Core
{
    public interface IParameter<T> : IParameter
    {
        new T Value { get; set; }
    }
}
