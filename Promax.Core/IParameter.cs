namespace Promax.Core
{
    public interface IParameter
    {
        string Name { get; set; }
        string Code { get; set; }
        string Unit { get; set; }
        object Value { get; set; }
    }
}
