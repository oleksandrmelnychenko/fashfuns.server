using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FashFuns.Common;
using FashFuns.Common.Exceptions.UserExceptions;
using FashFuns.Common.Helpers;
using FashFuns.Common.IdentityConfiguration;
using FashFuns.Domain.DataContracts.Identity;
using FashFuns.Domain.DbConnectionFactory;
using FashFuns.Domain.Entities.Identity;
using FashFuns.Domain.Repositories.Identity.Contracts;
using FashFuns.Services.IdentityServices.Contracts;
using Microsoft.IdentityModel.Tokens;

namespace FashFuns.Services.IdentityServices
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly IIdentityRepositoriesFactory _identityRepositoriesFactory;
        private readonly IDbConnectionFactory _connectionFactory;

        public UserIdentityService(
            IDbConnectionFactory connectionFactory,
            IIdentityRepositoriesFactory identityRepositoriesFactory)
        {
            _identityRepositoriesFactory = identityRepositoriesFactory;
            _connectionFactory = connectionFactory;
        }

        public Task<UserAccount> SignInAsync(AuthenticationDataContract authenticateDataContract) =>
            Task.Run(() =>
            {
                if (!Validator.IsEmailValid(authenticateDataContract.Email))
                    UserExceptionCreator<InvalidSignInException>.Create(
                        IdentityValidationMessages.EMAIL_INVALID,
                        SignInErrorResponseModel.New(SignInErrorResponseType.InvalidEmail,
                            IdentityValidationMessages.EMAIL_INVALID)).Throw();

                using (IDbConnection connection = _connectionFactory.NewSqlConnection())
                {
                    IIdentityRepository repository = _identityRepositoriesFactory.NewIdentityRepository(connection);
                    UserIdentity user = repository.GetUserByEmail(authenticateDataContract.Email);

                    if (user == null)
                    {
                        UserExceptionCreator<InvalidSignInException>.Create(
                            IdentityValidationMessages.INVALID_CREDENTIALS,
                            SignInErrorResponseModel.New(SignInErrorResponseType.InvalidCredentials,
                                IdentityValidationMessages.INVALID_CREDENTIALS)).Throw();
                    }

                    if (!CryptoHelper.Validate(authenticateDataContract.Password, user.PasswordSalt, user.PasswordHash))
                    {
                        UserExceptionCreator<InvalidSignInException>.Create(
                            IdentityValidationMessages.INVALID_CREDENTIALS,
                            SignInErrorResponseModel.New(SignInErrorResponseType.InvalidCredentials,
                                IdentityValidationMessages.INVALID_CREDENTIALS)).Throw();
                    }

                    var key = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings.TokenSecret);
                    var expiry = DateTime.UtcNow.AddDays(ConfigurationManager.AppSettings.TokenExpiryDays);
                    var claims = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Expiration, expiry.Ticks.ToString())
                        }
                    );

                    var isAdministrator =
                        user.UserRoles?.FirstOrDefault(r => r.UserRoleType.RoleType == RoleType.Administrator) != null;
                    if (isAdministrator)
                    {
                        claims.AddClaim(new Claim(ClaimTypes.Role, RoleType.Administrator.ToString()));
                        claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                    }

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Issuer = AuthOptions.ISSUER,
                        Audience = AuthOptions.AUDIENCE_LOCAL,
                        Subject = claims,
                        Expires = expiry,
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

                    var userData = new UserAccount(user)
                    {
                        TokenExpiresAt = expiry,
                        Token = tokenHandler.WriteToken(token),
                        IsAdministrator = isAdministrator
                    };

                    return userData;
                }
            });

        public Task<UserAccount> SignUpAsync(AuthorizationDataContract authenticateDataContract) =>
            Task.Run(() =>
            {
                using (IDbConnection connection = _connectionFactory.NewSqlConnection())
                {
                    IIdentityRepository repository = _identityRepositoriesFactory.NewIdentityRepository(connection);
                    
                    var exist = repository.GetUserByEmail(authenticateDataContract.Email);
                    if (exist != null) {
                        throw new ArgumentException(IdentityValidationMessages.ACCOUNT_ALREADY_EXIST);
                    }

                    IIdentityRolesRepository rolesRepository =
                        _identityRepositoriesFactory.NewIdentityRolesRepository(connection);

                    if (!Validator.IsEmailValid(authenticateDataContract.Email))
                        throw new ArgumentException(IdentityValidationMessages.EMAIL_INVALID);

                    if (!Regex.IsMatch(authenticateDataContract.Password,
                        ConfigurationManager.AppSettings.PasswordStrongRegex))
                    {
                        throw new ArgumentException(IdentityValidationMessages.PASSWORD_WEAK_ERROR);
                    }

                    string passwordSalt = CryptoHelper.CreateSalt();

                    string hashedPassword = CryptoHelper.Hash(authenticateDataContract.Password, passwordSalt);

                    UserIdentity currentUser =
                        repository.NewUser(authenticateDataContract.FullName,
                            authenticateDataContract.Email, hashedPassword, passwordSalt);

                    rolesRepository.AssignRoles(currentUser.Id, new List<long>() { 1 });

                    return SignInAsync(new AuthenticationDataContract
                        { Password = authenticateDataContract.Password, Email = authenticateDataContract.Email });
                }
            });


        private bool IsUserPasswordExpired(
           UserIdentity user)
        {
            if (user.IsPasswordExpired) { return true; }

            if (DateTime.UtcNow > user.PasswordExpiresAt)
            {
                user.IsPasswordExpired = true;
                return true;
            }

            return false;
        }

        private bool UpdatePassword(
           UserIdentity user,
           string newPassword,
           IIdentityRepository repository)
        {
            if (!Regex.IsMatch(newPassword, ConfigurationManager.AppSettings.PasswordStrongRegex))
            {
                //_logger.LogInformation("New password did not match minimum strong password requirements: {0}", ConfigurationManager.AppSettings.PasswordStrongRegex);
                return false;
            }

            if (user.CanUserResetExpiredPassword)
            {
                user.PasswordExpiresAt = DateTime.UtcNow.AddDays(ConfigurationManager.AppSettings.PasswordExpiryDays);
            }

            var salt = CryptoHelper.CreateSalt();
            user.PasswordSalt = salt;
            user.PasswordHash = CryptoHelper.Hash(newPassword, salt);
            user.IsPasswordExpired = false;

            repository.UpdateUserPassword(user);

            return true;
        }
    }
}
