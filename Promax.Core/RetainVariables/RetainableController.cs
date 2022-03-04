namespace Promax.Core
{
    public class RetainableController
    {
        public RetainableController(RetainableContainer retainableContainer, RetainableContainerSerializer retainableContainerSerializer)
        {
            RetainableContainer = retainableContainer;
            RetainableContainerSerializer = retainableContainerSerializer;
            RetainableContainerSerializer.Load(RetainableContainer);
            RetainableContainerSerializer.AutoSerialize(RetainableContainer);
        }

        public RetainableContainer RetainableContainer { get; private set; }
        public RetainableContainerSerializer RetainableContainerSerializer { get; private set; }

    }
}
