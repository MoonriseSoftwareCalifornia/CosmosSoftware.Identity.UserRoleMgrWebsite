using System.ComponentModel.DataAnnotations;

namespace CosmosSoftware.Identity.UserRoleMgrWebsite.Models
{
    public class IdentityUserCreateViewItem : IdentityUserViewItem
    {
        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
