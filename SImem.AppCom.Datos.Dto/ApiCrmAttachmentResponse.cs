using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class ApiCrmAttachmentResponse
    {
        public NoteAttachment? note_attachment { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class NoteAttachment
    {
        public string? id { get; set; }
        public string? filename { get; set; }
        public string? file { get; set; }
        public string? related_module_id { get; set; }
        public string? related_module_name { get; set; }
    }
}
