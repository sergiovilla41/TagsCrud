using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Simem.AppCom.Datos.Dto;
using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;

namespace Simem.AppCom.Base.Utils
{
    [ExcludeFromCodeCoverage]
    public class SeguridadHelper
    {
        private readonly JwtSecurityTokenHandler _jwtTokenHandler;
        private readonly SecurityKey _securityKey;

        public SeguridadHelper()
        {
            _jwtTokenHandler = new JwtSecurityTokenHandler();
            _securityKey = GenerateSecurityKey();
        }

        public string IssuingJWT(string user)
        {
            DateTime expirationDate = DateTime.UtcNow.AddMinutes(15);

            var claims = new List<Claim>
        {
            new Claim("username", user),
            new Claim("idGuid", Guid.NewGuid().ToString()),
            new Claim("fecha", expirationDate.ToString())
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.RsaSha256)
            };

            SecurityToken token = _jwtTokenHandler.CreateToken(tokenDescriptor);
            string tokenString = _jwtTokenHandler.WriteToken(token);

            return tokenString;
        }

        public string CreateJWT(string user, string code)
        {
            DateTime expirationDate = DateTime.UtcNow.AddYears(1);

            var claims = new List<Claim>
            {
                new Claim("user", user),
                new Claim("code", code)
            };

            var secretKey = KeyVaultManager.GetSecretValue(KeyVaultTypes.secretKeyJWT);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            };

            SecurityToken token = _jwtTokenHandler.CreateToken(tokenDescriptor);
            string tokenString = _jwtTokenHandler.WriteToken(token);

            return tokenString;
        }

        public static JwtDataDto ReadJWT(string token) {

            JwtDataDto jwtDataDto = new()
            {
                user = "",
                code = ""
            };

            try { 
                var secretKey = KeyVaultManager.GetSecretValue(KeyVaultTypes.secretKeyJWT);
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                var handler = new JwtSecurityTokenHandler();
                var validations = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                var claims = handler.ValidateTokenAsync(token, validations);
                object user;
                object code;
                claims.Result.Claims.TryGetValue("user", out user!);
                claims.Result.Claims.TryGetValue("code", out code!);
                jwtDataDto.user = (string)user;
                jwtDataDto.code = (string)code;

                return jwtDataDto;
            }
            catch (Exception) {
                return jwtDataDto;
            }
        }

        private static SecurityKey GenerateSecurityKey()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                var parameters = rsa.ExportParameters(true);
                return new RsaSecurityKey(parameters);
            }
        }
    }
}