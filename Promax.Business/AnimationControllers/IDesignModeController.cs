namespace Promax.Business
{
    public interface IDesignModeController
    {
        bool InDesignMode { get; }
        void EnterDesignMode();
        void ExitDesignMode();
    }
}
