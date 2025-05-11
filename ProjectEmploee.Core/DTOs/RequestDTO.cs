using ProjectEmploee.Core.Entities;
using ProjectEmploee.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEmploee.Core.DTOs
{
    public class RequestDTO
    {
        public int IdUser { get; set; }
        public ReasonForAbsence ReasonForAbsence { get; set; }
    }
}