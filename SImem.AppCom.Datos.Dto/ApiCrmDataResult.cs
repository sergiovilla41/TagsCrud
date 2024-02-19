using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class ApiCrmDataResult
    {
        public string? session { get; set; }
        public string? resp { get; set; }
        public string? start { get; set; }
        public string? end { get; set; }
        public string? total_count { get; set; }
        public string? limit { get; set; }
        public int next_offset { get; set; }
        public CasesList[]? cases_list { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class CasesList
    {
        public string? id { get; set; }
        public string? activo { get; set; }
        public string? case_number { get; set; }
        public string? type { get; set; }
        public string? ventana { get; set; }
        public string? direction { get; set; }
        public string? empresa { get; set; }
        public string? NIT { get; set; }
        public string? activity_type { get; set; }
        public string? contacto { get; set; }
        public object? cargo { get; set; }
        public object? tel1 { get; set; }
        public object? tel2 { get; set; }
        public string? asunto { get; set; }
        public object? cel1 { get; set; }
        public object? cel2 { get; set; }
        public string? email { get; set; }
        public object? cedula { get; set; }
        public string? grupo { get; set; }
        public string? description { get; set; }
        public string? status { get; set; }
        public string? responsable { get; set; }
        public string? procede { get; set; }
        public string? date_entered { get; set; }
        public string? date_modified { get; set; }
        public string? aig_reception_date { get; set; }
        public string? pendiente { get; set; }
        public object? aig_customer_response_date { get; set; }
        public string? customer_response_time { get; set; }
        public object? aig_response_date { get; set; }
        public string? dias { get; set; }
        public string? fuera_de_tiempo { get; set; }
        public string? tipo_recepcion { get; set; }
        public string? aig_siad_ref { get; set; }
        public object? tipo_envio { get; set; }
        public object? citese { get; set; }
        public object? resolution { get; set; }
        public object? aig_response { get; set; }
        public object? causal_asociada { get; set; }
        public string? justificacion { get; set; }
        public string? justificacion_corta { get; set; }
        public string? aig_root_cause { get; set; }
        public object? aig_incident_number { get; set; }
        public string? aig_invoice_claim { get; set; }
        public object? aig_radicated_number { get; set; }
        public string? priority { get; set; }
        public string? aig_type_solution { get; set; }
        public string? aig_survey_response { get; set; }
        public object? aig_survey_justification { get; set; }
        public string? aig_account_comm { get; set; }
       
        public string? date_entered_db { get; set; }
        public Attachment[]?  attachments { get; set; }

    }

    [ExcludeFromCodeCoverage]
    public class Attachment
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public string? date_entered { get; set; }
        public string? date_modified { get; set; }
        public string? created_by { get; set; }
        public string? modified_by { get; set; }
        public string? modified_by_username { get; set; }
        public string? url { get; set; }
    }

}
