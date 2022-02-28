using System.Collections.Generic;
using System.Linq;

namespace Utility.Triggers
{
    public class TriggerPerformer<T>
    {
        private List<Trigger<T>> _triggers = new List<Trigger<T>>();
        public Trigger<T> CreateTrigger()
        {
            var a = new Trigger<T>();
            _triggers.Add(a);
            return a;
        }
        public void Perform(TriggerInfo info, T newInstance, T oldInstance)
        {
            var a = _triggers.Where(x => x.TriggerType == info.TriggerType).ToList();
            a?.ForEach(t => t.Perform(info.TriggerActionType, newInstance, oldInstance));
        }
    }
}
