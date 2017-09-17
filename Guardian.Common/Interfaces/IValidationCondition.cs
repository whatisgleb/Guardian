namespace Guardian.Common.Interfaces
{
    public interface IValidationCondition
    {
        int ValidationConditionID { get; set; }
        string Expression { get; set; }
        string ApplicationID { get; set; }
    }
}