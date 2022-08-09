using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teste_Back_end_Predify2.Models;

namespace Teste_Back_end_Predify2.Mapper
{
    public class BusinessMapper
    {
        public BusinessDTO dto;

        public BusinessMapper(Business business)
        {
            dto = new BusinessDTO()
            {
                Id = business.Id,
                TradeName = business.TradeName,
                Cnpj = business.Cnpj,
                Uf = business.Uf
            };

        }
    }
}