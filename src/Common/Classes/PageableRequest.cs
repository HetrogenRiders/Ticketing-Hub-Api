namespace TicketingHub.Api.Common.Classes
{
    public class PageableRequest
    {
        // Property initializers to set default values
        public int PageNumber { get; set; } = 1;  // Default to the first page
        public int PageSize { get; set; } = 10;   // Default to 10 items per page
        public string SearchKey { get; set; } = string.Empty;  // Default to an empty search string
        public string SortDirection { get; set; } = "Ascending";  // Default to ascending order
    }
}
