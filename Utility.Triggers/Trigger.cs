using System.Text;
using System.Threading.Tasks;

namespace Utility.Triggers
{
    public class Trigger<T>
    {
        TriggerAction<T> _triggerAction;
        public TriggerType TriggerType { get; private set; }
        internal Trigger()
        {

        }
        public TriggerAction<T> Before()
        {
            return CreateTrigger(TriggerType.Before);
        }
        public TriggerAction<T> After()
        {
            return CreateTrigger(TriggerType.After);
        }
        public void Perform(TriggerActionType triggerActionType, T newInstance, T oldInstance)
        {
            _triggerAction?.Perform(triggerActionType, newInstance, oldInstance);
        }
        private TriggerAction<T> CreateTrigger(TriggerType triggerType)
        {
            TriggerType = triggerType;
            _triggerAction = new TriggerAction<T>();
            return _triggerAction;
        }
    }
}
