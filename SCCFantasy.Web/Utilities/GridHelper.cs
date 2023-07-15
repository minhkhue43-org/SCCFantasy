namespace SCCFantasy.Web.Utilities
{
    public static class GridHelper
    {
        public static IEnumerable<string> DefaultPageSizes()
        {
            return new string[] { "10", "25", "50", "100" };
        }
    }
}
