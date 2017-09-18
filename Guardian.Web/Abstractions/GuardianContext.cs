namespace Guardian.Web.Abstractions
{
    public abstract class GuardianContext
    {
        public GuardianDashboardRequest Request { get; protected set; }
        public GuardianDashboardResponse Response { get; protected set; }
    }
}
