using Application.DTOs;
using Application.DTOs.Users;
using Application.Enums;
using Application.Interfaces;
using Application.Wrrappers;
using Domain.Entities;
using Domain.Settings;
using identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Persistence.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userServices;
        private readonly JWTSettings _jWTSettings;
        private readonly IDateTimeService _dateTimeService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,
                IOptions<JWTSettings> jWTSettings, IDateTimeService dateTimeService,
                SignInManager<AppUser> signInManager, IUserService userServices)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jWTSettings = jWTSettings.Value;
            _dateTimeService = dateTimeService;
            _signInManager = signInManager;
            _userServices = userServices;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticateRequest request, string ipAdress)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new ArgumentException($"No Accounts Registered with {request.Email}");
            }

            var result = await _signInManager.PasswordSignInAsync(user.Email!, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new ArgumentException($"The email or password do not match.");
            }

            JwtSecurityToken securityToken = await GetJwtAsync(user);
            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = user.Id;
            response.Token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            response.Email = user.Email!;
            response.UserName = user.UserName!;

            var roleList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = roleList.ToList();
            response.IsAuthenticated = user.EmailConfirmed;

            var refreshToken = RefreshToken(ipAdress);
            response.RefreshToken = refreshToken.Token;

            return new Response<AuthenticationResponse>(response, message: "Login completed succesfully.");
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail != null)
                    throw new ArgumentException("That email already exists.");


            var identityUser = new AppUser
            {
                Email = request.Email,
                UserName = request.Email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            var result = await _userManager.CreateAsync(identityUser, request.Password);
            if (result.Succeeded)
            {

                var newDomainUser = new User
                {
                    Id = identityUser.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    BirthDate = request.BirthDate,
                    ContactNumber = request.ContactNumber,
                    Address = request.Address
                };

                await _userServices.AddUserAsync(newDomainUser);

                await _userManager.AddToRoleAsync(identityUser, Roles.Basic.ToString());
                return new Response<string>(identityUser.Id, message: "User created succesfully.");
            }
            else
            {
                return new Response<string>(result.Errors.Select(e => e.Description).FirstOrDefault()!);
            }
        }

        #region JWT Methods
        private async Task<JwtSecurityToken> GetJwtAsync(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleList = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleList.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            string ip = IpHelper.GetIp();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("ip", ip),
                new Claim("uid", user.Id),
                //new Claim(JwtRegisteredClaimNames.Iat, _dateTimeService.NowUtc.ToString(), ClaimValueTypes.Integer64)
            }.Union(userClaims).Union(roleList);

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.Key));

            var signInCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jWTSettings.Issuer,
                audience: _jWTSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jWTSettings.DurationInMinutes),
                signingCredentials: signInCredentials);

            return jwtSecurityToken;
        }

        private RefreshToken RefreshToken(string ip)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                ExpireTime = DateTime.UtcNow.AddDays(7),
                Created = _dateTimeService.NowUtc,
                CreatedByIp = ip
            };
        }

        private string RandomTokenString()
        {
            var randomBytes = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return BitConverter.ToString(randomBytes)
                 .Replace("+", "") // Remove '+' for more readability
                .Replace("/", "_") // Replace '/' and '+' for URL
                .TrimEnd('='); //
        }

        #endregion
    }
}
