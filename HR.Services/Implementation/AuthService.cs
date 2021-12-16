using HR.DAL;
using HR.DAL.Models;
using HR.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace HR.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Login(string email, string password)
        {
            var identity = await GetIdentity(email, password);
            if (identity == null)
            {
                throw new ArgumentException("Invalid username or password.");
            }

            var jwt = GenerateSecurityToken(AuthOptions.ISSUER, AuthOptions.AUDIENCE, identity);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return JsonSerializer.Serialize(response);
        }

        private static JwtSecurityToken GenerateSecurityToken(string ISSUER, string AUDIENCE, ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return jwt;
        }

        private async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            var person = await _context.Employees
                .Include(e => e.Role)
                .Include(e => e.Education)
                .Include(e => e.Adress)
                .FirstOrDefaultAsync(e => e.Email == email);

            if (person == null)
                return null;

            var checkPassword = await CheckPasswordAsync(person, password);

            if (person != null || checkPassword)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role.RoleName)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }

        private Task<bool> CheckPasswordAsync(Employee person, string password)
        {
            if (password != null && person.Password == password)
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public async Task RegisterNewUser(string email, string password, string university, string firstname, string lastname)
        {
            var person = await _context.Employees
                .Include(e => e.Role)
                .Include(e => e.Education)
                .Include(e => e.Adress)
                .FirstOrDefaultAsync(e => e.Email == email);

            if (person != null)
            {
                throw new ArgumentException("Email is already taken.");
            }

            Employee value = new Employee();
            value.Email = email;
            value.Password = password;
            value.FirstName = firstname;
            value.LastName = lastname;
            value.Role = _context.Roles.FirstOrDefault(x => x.RoleName == "User");
            value.Education = new Education();
            value.Education.University = university;
            value.Adress = new Address();

            var result = await _context.Employees.AddAsync(value);
            await _context.SaveChangesAsync();
        }
    }
}
