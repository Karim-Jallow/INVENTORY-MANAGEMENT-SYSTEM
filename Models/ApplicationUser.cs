using Microsoft.AspNetCore.Identity;
namespace INVENTORY_MANAGEMENT_SYSTEM.Models
{
    public class ApplicationUser : IdentityUser
    {
   public string ? FirstName { get; set; }
    public string ? MiddleName { get; set; }
    public string ? LastName { get; set; }
    public string ? Address { get; set; }
    public string ? Phone { get; set; }
    }
}
