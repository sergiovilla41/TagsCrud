using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simem.AppCom.Datos.Dominio
{
    [Table("Agent")]
    public class Agent
    {
        [Key()]
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public string? Icon { get; set; }
        public bool State { get; set; }
        public ICollection<Data>? Datas { get; set; }
    }
}
