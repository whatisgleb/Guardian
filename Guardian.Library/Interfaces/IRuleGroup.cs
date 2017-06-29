namespace Guardian.Library.Interfaces
{
    public interface IRuleGroup
    {
        string Expression { get; set; }
        string ErrorMessage { get; set; }
        string Key { get; set; }
    }
}