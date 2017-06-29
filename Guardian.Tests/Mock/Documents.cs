namespace Guardian.Tests.Mock
{
    public static class Documents
    {
        public static Document Document => new Document()
        {
            ID = 1,
            Title = "Guard this carefully!",
            Description = "Or else you might break something!",
        };
    }
}