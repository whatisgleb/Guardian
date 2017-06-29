namespace Guardian.Library.Interfaces
{
    public interface IRule
    {
        int ID { get; set; }
        string Expression { get; set; }
    }
}