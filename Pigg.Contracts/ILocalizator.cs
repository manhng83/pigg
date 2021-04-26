namespace Pigg.Contracts
{
    public interface ILocalizator
    {
        void SetCulture();

        string CountryCode { get; }
    }
}