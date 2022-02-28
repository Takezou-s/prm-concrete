namespace Promax.Core
{
    public interface IAnimationSerializer
    {
        void Deserialize(IAnimationObject[] animationObjects);
        void Serialize(IAnimationObject[] animationObjects);
    }
}