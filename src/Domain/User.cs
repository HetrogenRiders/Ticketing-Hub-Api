namespace TicketingHub.Api.Domain;
public class User
{
    public Guid UserID { get; set; }
    public string EmailId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? ResetPasswordExpiryTime {get; set;}
}