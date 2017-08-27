namespace Guardian.Common.Interfaces
{
    public interface IRuleGroup
    {
        int RuleGroupID { get; set; }
        string Expression { get; set; }
        string ErrorMessage { get; set; }
        int ErrorCode { get; set; }
        string ApplicationID { get; set; }
    }
}