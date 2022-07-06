using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CosmosSoftware.Identity.UserRoleMgrWebsite.Models
{
    public class IdentityUserViewItem
    {
        /// <summary>
        /// Empty constructor
        /// </summary>
        public IdentityUserViewItem()
        {

        }

        /// <summary>
        /// Constructor takes an identity user
        /// </summary>
        /// <param name="user"></param>
        public IdentityUserViewItem(IdentityUser user)
        {
            Id = user.Id;
            UserName = user.UserName;
            Email = user.Email;
            LockoutEnabled = user.LockoutEnabled;
            LockoutEnd = user.LockoutEnd;
            TwoFactorEnabled = user.TwoFactorEnabled;
            PhoneNumber = user.PhoneNumber;
            EmailConfirmed = user.EmailConfirmed;
            PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            AccessFailedCount = user.AccessFailedCount;
        }

        /// <summary>
        /// User Id
        /// </summary>
        [Key]
        [Required]
        [Display(Name = "User ID")]
        public string Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        [Display(Name = "User name")]
        [ProtectedPersonalData]
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// User email address
        /// </summary>
        [ProtectedPersonalData]
        [EmailAddress]
        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        /// <summary>
        /// End of lockout
        /// </summary>
        [Display(Name = "End of lockout")]
        public DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// Two factor authentication is enabled
        /// </summary>
        [Display(Name = "Two factor enabled")]
        [PersonalData]
        public bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// Phone number is confirmed
        /// </summary>
        [Display(Name = "Phone is confirmed")]
        [PersonalData]
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// User phone number
        /// </summary>
        [Display(Name = "Phone number")]
        [ProtectedPersonalData]
        [Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Email address is confirmed
        /// </summary>
        [Display(Name = "Email is confirmed")]
        [PersonalData]
        [Required]
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Lockout is enabled
        /// </summary>
        [Display(Name = "Lockout is enabled")]
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// Number of failed access attempts.
        /// </summary>
        [Display(Name = "Failed access count")]
        public int AccessFailedCount { get; set; }
    }
}
