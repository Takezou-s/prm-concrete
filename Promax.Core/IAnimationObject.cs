using System.Collections.Generic;

namespace Promax.Core
{
    public interface IAnimationObject
    {
        string AnimationObjectName { get; set; }
        object Props { get; }
        IEnumerable<string> EntityBindableProperties { get; }
    }
}