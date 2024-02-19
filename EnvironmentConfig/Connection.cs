using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Simem.AppCom.Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EnvironmentConfig
{
    public static class Connection
    {

        public static void SetEnvironmentVar()
        {
            Environment.SetEnvironmentVariable("AzureWebJobsStorage", "");
            Environment.SetEnvironmentVariable("FUNCTIONS_WORKER_RUNTIME", "dotnet");
            Environment.SetEnvironmentVariable("AzureStorage", "AzureStorage");
            Environment.SetEnvironmentVariable("JwtKey", "JwtKey");
            Environment.SetEnvironmentVariable("JwtExpirationTimeMins", "JwtExpirationTimeMins");
            Environment.SetEnvironmentVariable("maxDay", "maxDay");
            Environment.SetEnvironmentVariable("maxMonth", "maxMonth");
            Environment.SetEnvironmentVariable("maxYear", "maxYear");
            Environment.SetEnvironmentVariable("SimemConnection", "SimemConnectionTest");
            Environment.SetEnvironmentVariable("AzureKeyVaultUri", "https://kvsimemprb02.vault.azure.net/");
            Environment.SetEnvironmentVariable("clientId", "ZQA5ADQANAA2AGIANwAyAC0AOAA3AGMAYgAtADQANQAyADEALQA4ADkAYgBmAC0AZgAwAGYANQBhADAAYwA2AGEAZQAzADUA");
            Environment.SetEnvironmentVariable("clientSecret", "VgBHAGgAOABRAH4AVwBQAH4AQgBSAFQAUABqADgARQBNAHIAWABuAG0AZABIAHoAZQBiAEkAVgBCAEUAagBPAEMAVgBqAFgASQBjAFAAUgA=");
            Environment.SetEnvironmentVariable("tenantId", "YwA5ADgAMABlADQAMQAwAC0AMABiADUAYwAtADQAOABiAGMALQBiAGQAMQBhAC0AOABiADkAMQBjAGEAYgBjADgANABiAGMA");
            Environment.SetEnvironmentVariable("Pipeline", "false");
            Environment.SetEnvironmentVariable("user_pqr", "user-pqr");
            Environment.SetEnvironmentVariable("pass_pqr", "pass-pqr");
            Environment.SetEnvironmentVariable("PQEEndpoint", "PQEEndpoint");
            Environment.SetEnvironmentVariable("ClBuzonPQR", "ClBuzonPQR");
            Environment.SetEnvironmentVariable("BuzonPQR", "BuzonPQR");
            Environment.SetEnvironmentVariable("urlCrmLogin", "urlCrmLogin");
            Environment.SetEnvironmentVariable("urlCrmData", "urlCrmData");
            Environment.SetEnvironmentVariable("crmStatus", "crmStatus");
            Environment.SetEnvironmentVariable("crmModuleName", "crmModuleName");
            Environment.SetEnvironmentVariable("crmWindow", "crmWindow");
            Environment.SetEnvironmentVariable("crmWorkTeam", "crmWorkTeam");
            Environment.SetEnvironmentVariable("crmPriority", "crmPriority");
            Environment.SetEnvironmentVariable("secretKeyJWT", "secretKeyJWT");
            Environment.SetEnvironmentVariable("ContactanosFalla", "ContactanosFalla");
            Environment.SetEnvironmentVariable("DocumentalUser", "DocumentalUser");
            Environment.SetEnvironmentVariable("DocumentalUrl", "DocumentalUrl");
            Environment.SetEnvironmentVariable("DocumentalPass", "DocumentalPass");
            Environment.SetEnvironmentVariable("DocumentalClaseDocumental", "DocumentalClaseDocumental");
            Environment.SetEnvironmentVariable("DocumentalDebeResponder", "DocumentalDebeResponder");
            Environment.SetEnvironmentVariable("DocumentalDependencia", "DocumentalDependencia");
            Environment.SetEnvironmentVariable("DocumentalMedioRecibido", "DocumentalMedioRecibido");
            Environment.SetEnvironmentVariable("UrlContactanos", "UrlContactanos");
            Environment.SetEnvironmentVariable("StorageContainer", "StorageContainerSimem");

           
        }

        public static void ConfigureConnections()
        {
            SetEnvironmentVar();
            KeyVaultTypes[] enumValues = (KeyVaultTypes[])Enum.GetValues(typeof(KeyVaultTypes));
            byte[] decryted;

            decryted = Convert.FromBase64String(Environment.GetEnvironmentVariable("clientId")!);
            string clientId = Encoding.Unicode.GetString(decryted);

            decryted = Convert.FromBase64String(Environment.GetEnvironmentVariable("clientSecret")!);
            string clientSecret = Encoding.Unicode.GetString(decryted);

            decryted = Convert.FromBase64String(Environment.GetEnvironmentVariable("tenantId")!);
            string tenantId = Encoding.Unicode.GetString(decryted);

            var vaultUri = new Uri(Environment.GetEnvironmentVariable("AzureKeyVaultUri")!);
            ClientSecretCredential credential = new(tenantId, clientId, clientSecret);
            var client = new SecretClient(vaultUri, credential);

            foreach (var keyName in enumValues)
            {
                string secret = KeyVaultManager.GetSettingValue(keyName);

                if (secret != null) { 

                    if (!KeyVaultManager.IsPipelineVariableActive())
                    {
                        secret = client.GetSecret(secret).Value.Value;
                    }

                    KeyVaultManager.SetSecretValue(keyName.ToString(), secret);
                }
            }
        }
    }
}
