namespace Promax.Core
{
    public interface ICommandScope
    {
        void SetCommand(ICommander commander);
        bool IsCommandRetentive(object obj);
    }
}
