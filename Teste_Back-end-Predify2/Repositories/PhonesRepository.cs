using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teste_Back_end_Predify2.Data;
using Teste_Back_end_Predify2.Models;
using System.Threading.Tasks;

namespace Teste_Back_end_Predify2.Repositories
{
    public class PhonesRepository
    {
        private DataContext context = new DataContext();

        public async Task<Phone> Create(Phone phone)
        {
            context.Phones.Add(phone);

            context.SaveChanges();

            return phone;
        }
    }
}