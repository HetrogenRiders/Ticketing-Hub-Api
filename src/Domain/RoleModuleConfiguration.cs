namespace TicketingHub.Api.Domain
{
    public class RoleModuleConfiguration
    {
        public Guid RoleID { get; set; }
        public Guid ModuleID { get; set; }
        public bool? CanView { get; set; }

        public bool? CanAdd { get; set; }

        public bool? CanEdit { get; set; }

        public bool? CanDelete { get; set; }
        public bool IsDeleted { get; set; }
    }
}
