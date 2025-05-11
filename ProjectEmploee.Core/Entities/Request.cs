using ProjectEmploee.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEmploee.Core.Entities
{
    public class Request
    {
        [Key]
        public int IdRequesr { get; set; }
        [ForeignKey("idUser")]
        public int IdUser { get; set; }
        public ReasonForAbsence reasonForAbsence { get; set; }

    }
}
