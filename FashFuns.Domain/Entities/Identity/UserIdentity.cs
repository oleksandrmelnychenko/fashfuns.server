using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FashFuns.Domain.Entities.Identity {
    public class UserIdentity : EntityBase
    {
        public UserIdentity()
        {
            UserRoles = new HashSet<UserRole>();
        }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        public string Name { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        public bool IsPasswordExpired { get; set; } = false;

        public bool CanUserResetExpiredPassword { get; set; } = true;
     
        public DateTime PasswordExpiresAt { get; set; } = DateTime.UtcNow.AddYears(1);

        public long? PasswordExpiresAtTicks {
            get {
                if (PasswordExpiresAt != null)
                {
                    return PasswordExpiresAt.Ticks;
                }
                return null;
            }
        }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
