using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class ApiCrmCreateRequest
    {
        public string? session { get; set; }
        public ApiValueList? name_value_list { get; set; }
    }

    [ExcludeFromCodeCoverage]
	public class ApiValueList
    {
        public string? type { get; set; }
        public string? aig_reception_type { get; set; }
        public string? aig_reception_date { get; set; }
        public string? work_team { get; set; }
        public string? priority { get; set; }
        public string? name { get; set; }
        public string? aig_siad_ref { get; set; }
        public string? description { get; set; }
        public string? aig_contact_name { get; set; }
        public string? aig_contact_title { get; set; }
        public string? aig_account_name { get; set; }
        public string? aig_contact_phone_work { get; set; }
        public string? aig_contact_phone_other { get; set; }
        public string? aig_contact_phone_mobile { get; set; }
        public string? aig_contact_phone_mobile2 { get; set; }
        public string? aig_contact_email { get; set; }
        public string? aig_contact_id_number { get; set; }
        public string? attachment { get; set; }
    }
}
