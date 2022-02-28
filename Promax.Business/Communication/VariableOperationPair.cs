using System;

namespace Promax.Business
{
    [Serializable]
    class VariableOperationPair
    {
        public VariableOperationPair()
        {
        }

        public VariableOperationPair(object variable, string operation)
        {
            Variable = variable;
            Operation = operation;
        }

        public object Variable { get; set; }
        public string Operation { get; set; }

    }
}
