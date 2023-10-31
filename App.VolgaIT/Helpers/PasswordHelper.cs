using Domain.VolgaIT.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace App.VolgaIT.Helpers
{
    public class PasswordHelper
    {
        private readonly PasswordHashOptions _passwordHashOptions;

        public PasswordHelper(IOptions<PasswordHashOptions> passwordHashOptions)
        {
            _passwordHashOptions = new PasswordHashOptions();

        }

        public string GetPassword(string password)
        {
            using var encrypt = new Rfc2898DeriveBytes(password: password,
                saltSize: _passwordHashOptions.SaltSize,
                iterations: _passwordHashOptions.Iterations,
                hashAlgorithm: HashAlgorithmName.SHA256);
            var salt = Convert.ToBase64String(encrypt.Salt);

            var key = Convert.ToBase64String(encrypt.GetBytes(_passwordHashOptions.KeySize));

            return $"{_passwordHashOptions.Iterations}.{salt}.{key}";
        }

        public bool CheckPasswords(string userPassword, string password)
        {
            var passwordParts = userPassword.Split('.');

            var userSalt = Convert.FromBase64String(passwordParts[1]);

            var userKey = passwordParts[2];

            using var encrypt = new Rfc2898DeriveBytes(
            password: password,
                salt: userSalt,
                iterations: _passwordHashOptions.Iterations,
            hashAlgorithm: HashAlgorithmName.SHA256);

            var key = Convert.ToBase64String(encrypt.GetBytes(_passwordHashOptions.KeySize));
            return key == userKey;
        }
    }
}
