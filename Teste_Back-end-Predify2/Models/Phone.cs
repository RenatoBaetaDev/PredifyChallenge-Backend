using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Teste_Back_end_Predify2.Models
{
    public class Phone
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Number { get; set; }

        [Required]
        public string Type { get; set; }

        public int SupplierID { get; set; }

    }
}