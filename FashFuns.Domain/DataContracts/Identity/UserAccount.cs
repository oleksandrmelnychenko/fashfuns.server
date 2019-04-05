using System;
using FashFuns.Domain.Entities.Identity;

namespace FashFuns.Domain.DataContracts.Identity
{
    public class UserAccount
    {
        public UserAccount()
        {
        }

        public UserAccount(UserIdentity user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            IsPasswordExpired = user.IsPasswordExpired;
        }

        public string Email { get; set; }

        public long Id { get; set; }

        public bool? IsAdministrator { get; set; }

        public bool IsPasswordExpired { get; set; }

        public string Name { get; set; }

        public string Token { get; set; }

        public DateTime? TokenExpiresAt { get; set; }
    }
}
