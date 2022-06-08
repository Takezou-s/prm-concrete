namespace Promax.Process
{
    /// <summary>
    /// Malzeme alımı yapan ve bildiren objeler için arayüz.
    /// </summary>
    public interface IMalzemeAl
    {
        void MalzemeAl();
        bool MalzemeAlındı { get; }
        void ResetMalzemeAl();
    }
}
