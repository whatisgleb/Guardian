namespace Guardian.Tests.Mock
{
    public static class RuleGroups {
        public static RuleGroup Require_NonNullTitle = new RuleGroup() {
            ID = 1,
            Expression = "1",
            ApplicationName = "Demo",
            ErrorMessage = "A title is required",
            Key = "MemberNames.Title",
            ParameterType = "Document"
        };
        public static RuleGroup Required_StatusIsUploaded = new RuleGroup() {
            ID = 2,
            Expression = "2",
            ApplicationName = "Demo",
            ErrorMessage = "The document must be in an uploaded State",
            Key = "MemberNames.Status",
            ParameterType = "Document"
        };
        public static RuleGroup Require_PublicTag = new RuleGroup()
        {
            ID = 3,
            Expression = "3",
            ApplicationName = "Demo",
            ErrorMessage = "The document be tagged with a Public Tag",
            Key = "MemberNames.Tags",
            ParameterType = "Document"
        };
        public static RuleGroup Require_PublicTag_State = new RuleGroup()
        {
            ID = 4,
            Expression = "4",
            ApplicationName = "Demo",
            ErrorMessage = "The document be tagged with a Public Tag in an available state",
            Key = "MemberNames.Tags",
            ParameterType = "Document"
        };
        public static RuleGroup Require_TitleContains = new RuleGroup()
        {
            ID = 5,
            Expression = "5 && 6",
            ApplicationName = "Demo",
            ErrorMessage = "The document must have a title that contains 'test'",
            Key = "MemberNames.Title",
            ParameterType = "Document"
        };
    }
}
