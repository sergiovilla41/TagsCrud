using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Base.Utils
{
    public enum ErrorCodes
    {

        ArchivosInsert=100,
        ArchivosUpdate=101,
        ArchivosDelete=102,
        ArchivosUpload=103,
        ArchivosQuery =104,
        ArchivosNotFound = 105,
        ArchivoDownload = 106,
        ArchivoExists = 107,
    }
}
