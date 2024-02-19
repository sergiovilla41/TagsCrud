﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simem.AppCom.Base.Interfaces;
using Simem.AppCom.Datos.Repo;
using Simem.AppCom.Datos.Dto;
using Microsoft.AspNetCore.Mvc;



namespace Simem.AppCom.Datos.Core
{
    public class Etiqueta:IBaseTags
    {
        private readonly EtiquetaRepo repo;
        public Etiqueta()
        {
            repo ??= new EtiquetaRepo();
        }

        public List<EnlaceDto> GetTags()
        {
            return repo.GetTags();
        }
        public EnlaceDto GetTag(Guid idRegistry)
        {
            return repo.GetTag(idRegistry);
        }
        public Task NewTag(EnlaceDto entityDto)
        {
            return repo.NewTag(entityDto);
        }
        public Task DeleteTag(Guid idRegistry) { return repo.DeleteTag(idRegistry); }
        public Task<bool> ModifyTag(EnlaceDto entityDto) { return repo.ModifyTag(entityDto); }

        public List<ConjuntoDatosDto> GetDatosDto()
        {
            var datos = repo.GetDatosDto(); 

            return datos;
        }

        public List<ConjuntoDatosDto> GetDatosDtoById(Guid id)
        {
            var datosDto = repo.GetDatosDtoById(id); 

            return datosDto;
        }

    }
}