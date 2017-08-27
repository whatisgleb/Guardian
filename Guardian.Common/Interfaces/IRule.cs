namespace Guardian.Common.Interfaces
{
    public interface IRule
    {
        int RuleID { get; set; }
        string Expression { get; set; }
        string ApplicationID { get; set; }
    }
}