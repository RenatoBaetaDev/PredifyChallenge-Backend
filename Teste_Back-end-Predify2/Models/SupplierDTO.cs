using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Teste_Back_end_Predify2.Models
{
    public class SupplierDTO
    {
        public int Id;
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 11)]
        public string CpfCnpj { get; set; }

        public DateTime CreatedAt;
        public DateTime Birthdate;
        public string RG;
        public List<PhoneDTO> Phones;
    }
}