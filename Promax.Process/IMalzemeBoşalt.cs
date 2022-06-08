namespace Promax.Process
{
    /// <summary>
    /// Malzeme boşaltımı yapan ve bildiren objeler için arayüz.
    /// </summary>
    public interface IMalzemeBoşalt
    {
        void MalzemeBoşalt();
        bool MalzemeBoşaltıldı { get; }
        void ResetMalzemeBoşalt();
    }
}
