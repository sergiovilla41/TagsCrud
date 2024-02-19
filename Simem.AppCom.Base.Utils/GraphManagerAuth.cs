using Azure;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Base.Utils
{
    [ExcludeFromCodeCoverage]
    public static class GraphManagerAuth
    {
        public static HttpResponseMessage GetClientResponse(string email, string password)
        {
            byte[] decryted;


            decryted = Convert.FromBase64String(GetKeyValue("clientId"));
            string clientId = System.Text.Encoding.Unicode.GetString(decryted);

            decryted = Convert.FromBase64String(GetKeyValue("clientSecret"));
            string clientSecret = System.Text.Encoding.Unicode.GetString(decryted);

            decryted = Convert.FromBase64String(GetKeyValue("tenantId"));
            string tenantId = System.Text.Encoding.Unicode.GetString(decryted);

            // Azure AD OAuth 2.0 token endpoint
            string tokenEndpoint = $"https://login.microsoftonline.com/{tenantId}/oauth2/token";
            string resource = "https://graph.microsoft.com";

            using (HttpClient client = new())
            {
                // Formulate the request payload
                FormUrlEncodedContent requestContent = FormUrlEncodedContent(clientId, clientSecret, resource, email, password);

                // Send the POST request to Azure AD token endpoint
                return client.PostAsync(tokenEndpoint, requestContent).Result;
            }
        }

        private static FormUrlEncodedContent FormUrlEncodedContent(string clientId, string clientSecret, string resource, string email, string password)
        {
            return new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("resource", resource),
                new KeyValuePair<string, string>("scope", "openid"),
                new KeyValuePair<string, string>("username", email),
                new KeyValuePair<string, string>("password", password)
            });
        }

        public static string ValidateToken(string accessToken)
        {
            string response = "";
            // Parse the token
            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(accessToken);

            // Get the expiration time from the token's payload
            DateTime expirationTime = jwtToken.ValidTo;
            // Get the current time
            DateTime currentTime = DateTime.UtcNow;

            // Compare the expiration time to the current time
            if(currentTime < expirationTime)
            {
               string? email =  jwtToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
               
               response = !string.IsNullOrEmpty(email) ? email : "";
            }
            else
            {
                response = "expired";
            }

            return response;
        }

        public static string GetKeyValue(string value)
        {

            string? key = Environment.GetEnvironmentVariable(value);

            return key?? "";
        }
    
    }
}
