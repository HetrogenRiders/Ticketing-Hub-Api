namespace TicketingHub.Api.Features.Admin.Configuration.SubCategoryManagement.GetByIdSubCategory
{
    public class GetSubCategoryByIdResponse
    {
        public Guid CategoryId { get; set; }
        public string? SubCategoryName { get; set; }
        public bool IsActive { get; set; }
        public string? Description { get; set; }
    }
}
