namespace Promax.Process
{
    public interface IKarıştır
    {
        void Karıştır();
        bool Karıştırıldı { get; }
        void ResetKarıştır();
    }
}
