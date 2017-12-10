namespace Guardian.Core.Interfaces
{
    public interface IIdentifier : IToken
    {
        int ID { get; }
    }
}