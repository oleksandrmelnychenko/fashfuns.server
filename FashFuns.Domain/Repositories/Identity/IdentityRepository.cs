using System;
using System.Data;
using System.Linq;
using Dapper;
using FashFuns.Domain.Entities.Identity;
using FashFuns.Domain.Repositories.Identity.Contracts;

namespace FashFuns.Domain.Repositories.Identity {
    public class IdentityRepository : IIdentityRepository {
        private readonly IDbConnection _connection;

        public IdentityRepository(IDbConnection connection) {
            _connection = connection;
        }

        public void DeleteUserById(long userId) {
            _connection.Execute("DELETE FROM [UserIdentities] WHERE Id=@Id", new { Id = userId });
        }

        public UserIdentity GetUserByEmail(string email) {
            UserIdentity userIdentity = null;

            _connection.Query<UserIdentity, UserRole, UserIdentityRoleType, UserIdentity>(
                 "SELECT * FROM [UserIdentities] " +
                 "LEFT JOIN [UserRoles] on [UserRoles].UserIdentityId = [UserIdentities].Id " +
                 "LEFT JOIN [UserIdentityRoleTypes] ON [UserIdentityRoleTypes].Id = [UserRoles].UserRoleTypeId " +
                 "WHERE Email = @Email",
                 (user, role, roleType) => {
                     if (userIdentity != null) {
                         if (role != null) {
                             userIdentity.UserRoles.Add(role);
                         }
                     } else {
                         if (role != null) {
                             if (roleType != null) {
                                 role.UserRoleType = roleType;
                             }

                             user.UserRoles.Add(role);
                         }

                         userIdentity = user;
                     }

                     return user;
                 },
                 new { Email = email }
                 );

            return userIdentity;
        }

        public UserIdentity GetUserById(long userId) =>
            _connection.Query<UserIdentity>(
                 "SELECT * FROM UserIdentities " +
                 "WHERE Id = @Id",
                  new { Id = userId }).SingleOrDefault();

        public UserIdentity NewUser(string name, string email, string passwordHash, string passwordSalt) =>
                _connection.Query<UserIdentity>(
                "INSERT INTO [UserIdentities] (IsDeleted,IsPasswordExpired,CanUserResetExpiredPassword,Name,Email,PasswordHash,PasswordSalt,PasswordExpiresAt) " +
                "VALUES(0,0,0,@Name,@Email,@PasswordHash,@PasswordSalt,@PasswordExpiresAt) " +
                "SELECT * FROM [UserIdentities] WHERE ID = (SELECT SCOPE_IDENTITY()) ",
                new {
                    Name = name,
                    Email = email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    PasswordExpiresAt = DateTime.Now.AddDays(1)
                }
        ).SingleOrDefault();

        public long UpdateUserExperationDate(long userId, bool isExpired) =>
            _connection.Execute(
                "UPDATE UserIdentities Set IsPasswordExpired = @IsExpired, LastModified = getutcdate() " +
                "WHERE Id = @Id",
                new { Id = userId, IsExpired = isExpired }
                );

        public int UpdateUserPassword(UserIdentity user) =>
            _connection.Execute(
                "UPDATE UserIdentities Set " +
                "PasswordExpiresAt = @PasswordExpiresAt, IsPasswordExpired = @IsPasswordExpired, PasswordSalt = @PasswordSalt, PasswordHash = @PasswordHash, LastModified = getutcdate() " +
                "WHERE Id = @Id",
                new {
                    Id = user.Id,
                    PasswordExpiresAt = user.PasswordExpiresAt,
                    IsPasswordExpired = user.IsPasswordExpired,
                    PasswordSalt = user.PasswordSalt,
                    PasswordHash = user.PasswordHash
                });
    }
}
