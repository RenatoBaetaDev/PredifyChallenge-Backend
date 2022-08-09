using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teste_Back_end_Predify2.Models;
using Teste_Back_end_Predify2.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Teste_Back_end_Predify2.Mapper;

namespace Teste_Back_end_Predify2.Repositories
{
    public class BusinessRepository
    {
        private DataContext context = new DataContext();

        public async Task<List<BusinessDTO>> Get()
        {
            return await context.Businesses
                .Select(b => new BusinessDTO()
                {
                    Id = b.Id,
                    TradeName = b.TradeName,
                    Cnpj = b.Cnpj,
                    Uf = b.Uf,
                    Suppliers = b.BusinessSupplier.Select(
                        bs => new SupplierDTO
                        {
                            Id = bs.Supplier.Id,
                            Name = bs.Supplier.Name,
                            CpfCnpj = bs.Supplier.CpfCnpj,
                            RG = bs.Supplier.RG
                        }
                    ).ToList(),
                }).ToListAsync();
        }

        public async Task<BusinessDTO> Get(int id)
        {
            return await context.Businesses
                .Select(b => new BusinessDTO()
                {
                    Id = b.Id,
                    TradeName = b.TradeName,
                    Cnpj = b.Cnpj,
                    Uf = b.Uf,
                    Suppliers = b.BusinessSupplier.Select(
                        bs => new SupplierDTO
                        {
                            Id = bs.Supplier.Id,
                            Name = bs.Supplier.Name,
                            CpfCnpj = bs.Supplier.CpfCnpj,
                            RG = bs.Supplier.RG
                        }
                    ).ToList(),
                }).SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<BusinessDTO> Create(BusinessDTO businessDTO)
        {
            Business business = new Business() {
                TradeName = businessDTO.TradeName,
                Cnpj = businessDTO.Cnpj,
                Uf = businessDTO.Uf
            };

            context.Businesses.Add(business);
            context.SaveChanges();

            if (businessDTO.Suppliers != null)
            {
                SupplierRepository supplierRepository = new SupplierRepository();

                foreach (SupplierDTO currentSupplier in businessDTO.Suppliers)
                {
                    SupplierDTO supplierDTO = currentSupplier;

                    if (currentSupplier.Id > 0)
                    {
                        supplierDTO = supplierRepository.Get(currentSupplier.Id);
                    } else
                    { 
                        supplierDTO = supplierRepository.Create(supplierDTO);
                    }

                    if (business.Uf == "São Paulo")
                    {
                        DateTime today = DateTime.Today;
                        int age = today.Year - supplierDTO.Birthdate.Year;
                        if (age < 18)
                        {
                            return null;
                        }
                    }

                    BusinessSupplier relation = new BusinessSupplier()
                    {
                        BusinessId = business.Id,
                        SupplierId = supplierDTO.Id,
                    };

                    context.BusinessSuppliers.Add(relation);
                }
            }

            context.SaveChanges();

            BusinessMapper mapper = new BusinessMapper(business);
            return mapper.dto;
        }


        public async Task<BusinessDTO> Update(Business business)
        {
            context.Businesses.Update(business);

            context.SaveChanges();

            BusinessMapper mapper = new BusinessMapper(business);
            return mapper.dto;
        }

        public async Task<Business> Delete(int id)
        {
            Business business = await context.Businesses.FindAsync(id);

            if (business == null) return business;

            context.Businesses.Remove(business);
            await context.SaveChangesAsync();

            return business;
        }
    }
}