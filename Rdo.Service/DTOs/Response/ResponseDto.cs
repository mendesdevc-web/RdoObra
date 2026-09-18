using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rdo.Service.DTOs.Response
{
    public class ResponseDto<T>
    {
        public T? Dados { get; set; }
        public string Messagem { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
    }
}
