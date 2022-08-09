using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teste_Back_end_Predify2.Models
{
    public class BusinessSupplier
    {
        public int BusinessId { get; set; }
        public Business Business { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}