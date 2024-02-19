using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simem.AppCom.Datos.Dto;

namespace Simem.AppCom.Base.Interfaces
{
    public interface IBaseTags
    {
        public List<EnlaceDto> GetTags();
        public EnlaceDto GetTag(Guid idRegistry);
        public Task NewTag(EnlaceDto entityDto);
        public Task DeleteTag(Guid idRegistry);
        public Task<bool> ModifyTag(EnlaceDto entityDto);
    }
}
