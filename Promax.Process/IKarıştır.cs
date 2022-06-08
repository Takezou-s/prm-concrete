namespace Promax.Process
{
    /// <summary>
    /// Karıştırma işlemi yapan ve bildiren objeler için arayüz.
    /// </summary>
    public interface IKarıştır
    {
        void Karıştır();
        bool Karıştırıldı { get; }
        void ResetKarıştır();
    }
}
