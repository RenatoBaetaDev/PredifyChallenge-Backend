using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Teste_Back_end_Predify2.Models
{
    public class BusinessDTO
    {
        public int Id { get; set; }

        [Required]
        public string TradeName { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 14)]
        public string Cnpj { get; set; }

        [Required]
        public string Uf { get; set; }
        public List<SupplierDTO> Suppliers;
    }
}