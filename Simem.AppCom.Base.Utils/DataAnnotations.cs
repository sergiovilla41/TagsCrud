using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Simem.AppCom.Base.Utils
{
    [ExcludeFromCodeCoverage]
    public static class DataAnnotations
    {
        /// <summary>
        /// Metodo para validar DataAnnotations en los modelos de entity
        /// </summary>
        /// <param name="o"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static bool IsValid(this object o, out ICollection<ValidationResult> validationResults)
        {
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(o, new ValidationContext(o, null, null), validationResults, true);
        }
    }
}
