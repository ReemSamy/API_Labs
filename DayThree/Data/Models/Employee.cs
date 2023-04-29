using Microsoft.AspNetCore.Identity;

namespace DayThree.Data.Models
{
    public class Employee : IdentityUser
    {
        public string Department { get; set; } = string.Empty;
    }
}
