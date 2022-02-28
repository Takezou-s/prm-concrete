namespace Promax.Business
{
    public class VariableStack
    {
        public ushort StartingIndex { get; private set; }
        public ushort Count { get; private set; }
        public VariableStack()
        {

        }

        public VariableStack(ushort startingIndex, ushort count)
        {
            StartingIndex = startingIndex;
            Count = count;
        }
    }
}
