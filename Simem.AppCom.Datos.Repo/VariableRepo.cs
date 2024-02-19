using AutoMapper;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapeos;
using Microsoft.EntityFrameworkCore;
using Simem.AppCom.Datos.Dominio;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Simem.AppCom.Datos.Repo
{
    public class VariableRepo: IVariableRepo
    {
        private readonly DbContextSimem _baseContext;

        public VariableRepo()
        {
            _baseContext = new DbContextSimem();
        }

       public List<ConfiguracionVariableDto> GetVariables()
        {
            List<ConfiguracionVariableDto> lista = new();

            try
            {
                var obj = _baseContext.ConfiguracionVariable.ToList();
                if(obj != null)
                {
                   var dto = MapeoDatos.Mapper.Map<List<ConfiguracionVariableDto>>(obj);
                   lista.AddRange(dto);
                }

            }catch(Exception) {
             //Devuelva objeto vacio en caso de que falle
            }


            return lista.OrderBy(str => str.NombreVariable?.Trim()).ToList();
        }

        public InventarioVariablesResultDto GetVariablesFilteredByTitle(string texto)
        {            
            InventarioVariablesResultDto result = new();
            List<ConfiguracionVariableDto> lista = new();
            try
            {                
                string cleanText = texto=="Ñ" ? texto.ToLower() : RemoveAccents(texto).ToLower();

                var Contains = _baseContext.ConfiguracionVariablePrcResult
               .FromSqlInterpolated($"EXEC configuracion.sp_BuscarInventarioVariable {cleanText}")
               .ToList();

                if (Contains.Count > 0)
                {
                    foreach (var item in Contains)
                    {
                        var dto = MapeoDatos.Mapper.Map<ConfiguracionVariableDto>(item);
                        if (!string.IsNullOrEmpty(item.etiquetaNombre))
                        {
                            string[] etiquetas = item.etiquetaNombre.Split(",");

                            foreach (var etiqueta in etiquetas)
                            {
                                dto.Etiquetas.Add(new ConfiguracionVariableDto.EtiquetasVariablesDto
                                {
                                    id = null,
                                    Nombre = etiqueta
                                });
                            }

                        }

                        lista.Add(dto);
                    }
                }

                result.totalRecord = _baseContext.ConfiguracionVariable.Count();
                result.result = !string.IsNullOrEmpty(texto) ? lista : lista.OrderBy(str => str.NombreVariable?.Trim()).ToList();


            }
            catch (Exception)
            {
                //Devuelva objeto vacio en caso de que falle
            }


            return result;
        }

        public static string RemoveAccents(string input)
        {
            // Normalize the input string to Unicode normalization form D (NFD)
            string normalizedString = input.Normalize(NormalizationForm.FormD);

            // Create a StringBuilder to store the result without diacritics
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                // Get the Unicode category of the current character
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]);

                // Append the character to the result if it is not a diacritic
                if (category != UnicodeCategory.NonSpacingMark)
                {
                    result.Append(normalizedString[i]);
                }
            }

            return result.ToString();
        }

        public List<ConfiguracionVariableDto> GetVariableById(Guid id)
        {
            List<ConfiguracionVariableDto> Variable = new();

            try
            {

                var obj = _baseContext.ConfiguracionVariable.FirstOrDefault(x => x.IdVariable == id);
                if (obj != null)
                    Variable.Add(MapeoDatos.Mapper.Map<ConfiguracionVariableDto>(obj));

            }catch(Exception) { 
             // Devuelve objeto vacio en caso que falle
            }


            return Variable;
        }

    }
}
