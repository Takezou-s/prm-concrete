using System;
using System.Collections.Generic;

namespace Utility.Triggers
{
    public class TriggerAction<T>
    {
        private List<TriggerActionType> _list = new List<TriggerActionType>();
        private Action<T, T> _action;
        internal TriggerAction()
        {

        }
        public TriggerAction<T> Insert()
        {
            return CreateState(TriggerActionType.Insert);
        }
        public TriggerAction<T> Delete()
        {
            return CreateState(TriggerActionType.Delete);
        }
        public TriggerAction<T> Update()
        {
            return CreateState(TriggerActionType.Update);
        }
        public void Action(Action<T, T> action)
        {
            _action = action;
        }
        public void Perform(TriggerActionType triggerActionType, T newInstance, T oldInstance)
        {
            if (_action == null)
                return;
            bool inList = false;
            foreach (var item in _list)
            {
                if (item == triggerActionType)
                {
                    inList = true;
                    break;
                }
            }
            if (inList)
            {
                _action(newInstance, oldInstance);
            }
        }
        private TriggerAction<T> CreateState(TriggerActionType triggerType)
        {
            _list.Add(triggerType);
            return this;
        }
    }
}
