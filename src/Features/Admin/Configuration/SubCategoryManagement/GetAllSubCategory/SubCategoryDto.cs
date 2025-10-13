namespace TicketingHub.Api.Features.Admin.Configuration.SubCategoryManagement.GetAllSubCategory
{
    public class SubCategoryDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; } 
        public bool IsActive { get; set; }
    }
}