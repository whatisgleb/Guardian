namespace Guardian.Core.Interfaces
{
    internal interface IIdentifier : IToken
    {
        int ID { get; }
    }
}