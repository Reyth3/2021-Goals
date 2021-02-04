using BC.Server.Models.DB;
using BC.Shared;
using BC.Shared.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BC.Server.Services
{
    public class PBKDF2UserValidationService : IUserValidationService
    {
        private readonly ILogger<PBKDF2UserValidationService> _logger;
        private readonly DataContext _context;

        public PBKDF2UserValidationService(ILogger<PBKDF2UserValidationService> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<UserProfile> RegisterUser(RegistrationVM request)
        {
            var match = _context.UserProfiles.Where(o => o.EmailAddress == request.EmailAddress).FirstOrDefault();
            if (match != null)
                throw new InvalidOperationException("The user with the specified email already exists.");
            match = new Shared.Models.UserProfile();
            match.EmailAddress = request.EmailAddress;
            match.FirstName = request.FirstName;
            match.LastName = request.LastName;

            // Finally what we've all been waiting for...
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            var saltString = Convert.ToBase64String(salt);
            match.Salt = saltString;
            var hashed = ComputeHash(request.Password, salt);

            match.Password = hashed;
            _context.UserProfiles.Add(match);
            await _context.SaveChangesAsync();
            return match;
        }

        public async Task<UserProfile> LogInUser(LogInVM request)
        {
            var match = _context.UserProfiles.Where(o => o.EmailAddress == request.Email).FirstOrDefault();
            if (match == null)
                throw new InvalidOperationException("Incorrect email address.");
            var hashed = ComputeHash(request.Password, Convert.FromBase64String(match.Salt));
            if (match.Password == hashed)
                return match;
            throw new InvalidOperationException("The password was incorrect.");
        }

        private string ComputeHash(string password, byte[] salt)
        {
            if (string.IsNullOrEmpty(password) || salt == null || salt.Length == 0)
                throw new ArgumentNullException("Password & salt required.");
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}
