namespace RemoteVariableHandler.Core
{
    public interface IVariableBuilder
    {
        IRemoteVariable GetVariable<T>();
        IRemoteVariable<T> GetVariableAsGeneric<T>();
        IVariableBuilder SetDefinition(IVariableDefinition definition);
        IVariableBuilder SetWriteValue<T>(T value);
        IVariableBuilder SetReadValue<T>(T value);
        IVariableBuilder Reset();
    }
}