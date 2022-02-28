using Extensions;
using RemoteVariableHandler.Core;
using System.Linq;

namespace Promax.Core
{
    public class ParameterOwner : ParameterOwnerBase
    {
        public ParameterOwner(string name, string displayName) : base(name, displayName)
        {
        }
        public override void InitParameters()
        {

        }
        public ParameterOwner AddParameter(IParameter parameter)
        {
            Parameters.FirstOrDefault(x => x.Name.IsEqual(parameter.Name)).Do(x => {; }, () => Parameters.Add(parameter));
            return this;
        }
        public ParameterOwner AddParameter<T>(string name, T value, string unit)
        {
            Parameters.FirstOrDefault(x => x.Name.IsEqual(name)).Do(x => {; }, () => Parameters.Add(new Parameter<T>(name, value, unit)));
            return this;
        }
        public ParameterOwner AddParameter<T>(string name, T value, string unit, string code)
        {
            Parameters.FirstOrDefault(x => x.Name.IsEqual(name)).Do(x => {; }, () => Parameters.Add(new Parameter<T>(name, value, unit, code)));
            return this;
        }
    }
}
