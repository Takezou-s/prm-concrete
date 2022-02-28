namespace Promax.Core
{
    public class RetainVariableController
    {
        private Serializer _serializer;
        private object _retainVariables;

        public RetainVariableController(Serializer serializer, object retainVariables)
        {
            _serializer = serializer;
            _retainVariables = retainVariables;
            serializer.Populate(retainVariables);
            serializer.AutoSerialize(retainVariables);
        }
    }
}
