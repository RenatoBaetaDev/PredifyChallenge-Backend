using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teste_Back_end_Predify2.Models;
using Teste_Back_end_Predify2.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Teste_Back_end_Predify2.Repositories
{
    public class SupplierRepository
    {
        private DataContext context = new DataContext();

        public async Task<List<SupplierDTO>> Get()
        {
            return await context.Suppliers
                .Select(
                s => new SupplierDTO()
                {
                    Id = s.Id,
                    Name = s.Name,
                    CpfCnpj = s.CpfCnpj,
                    RG = s.RG,
                    Phones = s.Phones.Select(
                        p => new PhoneDTO
                        {
                            Id = p.Id,
                            Number = p.Number,
                            Type = p.Type
                        }
                    ).ToList(),
                }
            ).ToListAsync();
        }

        public SupplierDTO Get(int id)
        {
            return context.Suppliers
                .Select(
                s => new SupplierDTO()
                {
                    Id = s.Id,
                    Name = s.Name,
                    CpfCnpj = s.CpfCnpj,
                    RG = s.RG,
                    Birthdate = s.Birthdate,
                    Phones = s.Phones.Select(
                        p => new PhoneDTO
                        {
                            Id = p.Id,
                            Number = p.Number,
                            Type = p.Type
                        }
                    ).ToList(),
                }).SingleOrDefault(s => s.Id == id);
        }

        public async Task<List<SupplierDTO>> GetByName(string name)
        {
            return await context.Suppliers
                .Select(
                s => new SupplierDTO()
                {
                    Id = s.Id,
                    Name = s.Name,
                    CpfCnpj = s.CpfCnpj,
                    RG = s.RG,
                    Phones = s.Phones.Select(
                        p => new PhoneDTO
                        {
                            Id = p.Id,
                            Number = p.Number,
                            Type = p.Type
                        }
                    ).ToList(),
                }).Where(s => s.Name.Contains(name)).ToListAsync();
        }

        public async Task<List<SupplierDTO>> GetByCpfCnpj(string CpfCnpj)
        {
            return await context.Suppliers
                .Select(
                s => new SupplierDTO()
                {
                    Id = s.Id,
                    Name = s.Name,
                    CpfCnpj = s.CpfCnpj,
                    RG = s.RG,
                    Phones = s.Phones.Select(
                        p => new PhoneDTO
                        {
                            Id = p.Id,
                            Number = p.Number,
                            Type = p.Type
                        }
                    ).ToList(),
                }).Where(s => s.CpfCnpj.Contains(CpfCnpj)).ToListAsync();
        }

        public async Task<List<SupplierDTO>> GetByDate(DateFilterDTO dateFilter)
        {
            DateTime iDate = Convert.ToDateTime(dateFilter.initialDate);

            var query = context.Suppliers
                .Where(s => s.CreatedAt >= iDate)
                .Select(
                s => new SupplierDTO()
                {
                    Id = s.Id,
                    Name = s.Name,
                    CpfCnpj = s.CpfCnpj,
                    RG = s.RG,
                    Phones = s.Phones.Select(
                        p => new PhoneDTO
                        {
                            Id = p.Id,
                            Number = p.Number,
                            Type = p.Type
                        }
                    ).ToList(),
                });

            if (dateFilter.finalDate != null)
            {
                DateTime fDate = Convert.ToDateTime(dateFilter.finalDate);
                query = query.Where(s => s.CreatedAt <= fDate);
            }

            return await query.ToListAsync();
        }

        public async Task<Supplier> Update(Supplier supplier)
        {
            context.Suppliers.Update(supplier);

            await context.SaveChangesAsync();

            return supplier;
        }

        public SupplierDTO Create(SupplierDTO supplierDTO)
        {
            Supplier supplier = new Supplier()
            {
                Name = supplierDTO.Name,
                CpfCnpj = supplierDTO.CpfCnpj,
                Birthdate = supplierDTO.Birthdate,
                RG = supplierDTO.RG
            };

            if (supplier.CpfCnpj.Length == 11 && (supplier.RG == null || supplier.Birthdate == null)) return null;

            context.Suppliers.Add(supplier);

            context.SaveChanges();

            supplierDTO.Id = supplier.Id;

            return supplierDTO;
        }


        public async Task<Supplier> Delete(int id)
        {
            Supplier supplier = await context.Suppliers.FindAsync(id);

            if (supplier == null) return supplier;

            context.Suppliers.Remove(supplier);
            await context.SaveChangesAsync();

            return supplier;
        }
    }
}