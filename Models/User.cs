using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibApp.Models
{
    public class User : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Firstname { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
    }
}
