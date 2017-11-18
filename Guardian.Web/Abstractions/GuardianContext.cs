namespace Guardian.Web.Abstractions
{
    public abstract class GuardianContext
    {
        public GuardianOptions Options { get; protected set; }
        public GuardianRequest Request { get; protected set; }
        public GuardianResponse Response { get; protected set; }
    }
}
