using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Simem.AppCom.Base.Utils
{
    [ExcludeFromCodeCoverage]
    public static class KeyVaultManager
    {

        #region Methods
        /// <summary>
        /// Gets the 'value' data saved into azure KeyVault configuration.
        /// </summary>
        /// <returns></returns>
        public static string GetSecretValue(KeyVaultTypes name)
        {
            if (!IsPipelineVariableActive())
            {
                if (Cache.TryGetValue(name.ToString(), out var value))
                {
                    return value;
                }
                return "";
            }

            return GetSettingValue(name);
        }

        public static string GetSettingValue(KeyVaultTypes type)
        {
            return Environment.GetEnvironmentVariable(type.ToString())!;
        }

        public static bool IsPipelineVariableActive() => Environment.GetEnvironmentVariable("Pipeline")?.ToUpper() == "TRUE";
        #endregion

        // Define una variable estática para almacenar los valores de las claves
        private static readonly Dictionary<string, string> Cache = new();


        public static void SetSecretValue(string keyName, string value)
        {
            Cache[keyName] = value;
        }
    }
}
