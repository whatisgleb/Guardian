namespace Guardian.Web.Abstractions
{
    public abstract class GuardianContext
    {
        public GuardianOptions Options { get; protected set; }
        public GuardianDashboardRequest Request { get; protected set; }
        public GuardianDashboardResponse Response { get; protected set; }
    }
}
