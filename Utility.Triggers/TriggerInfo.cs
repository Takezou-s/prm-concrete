namespace Utility.Triggers
{
    public class TriggerInfo
    {
        public static TriggerInfo Get()
        {
            return new TriggerInfo();
        }
        public TriggerType TriggerType { get; private set; }
        public TriggerActionType TriggerActionType { get; private set; }
        public TriggerInfo Before()
        {
            TriggerType = TriggerType.Before;
            return this;
        }
        public TriggerInfo After()
        {
            TriggerType = TriggerType.After;
            return this;
        }
        public TriggerInfo Insert()
        {
            TriggerActionType = TriggerActionType.Insert;
            return this;
        }
        public TriggerInfo Update()
        {
            TriggerActionType = TriggerActionType.Update;
            return this;
        }
        public TriggerInfo Delete()
        {
            TriggerActionType = TriggerActionType.Delete;
            return this;
        }
        private TriggerInfo()
        {

        }

        private TriggerInfo(TriggerType triggerType, TriggerActionType triggerActionType)
        {
            TriggerType = triggerType;
            TriggerActionType = triggerActionType;
        }
    }
}
