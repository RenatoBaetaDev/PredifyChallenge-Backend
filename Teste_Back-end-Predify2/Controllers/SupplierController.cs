using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Teste_Back_end_Predify2.Repositories;
using Teste_Back_end_Predify2.Models;
using System.Threading.Tasks;

namespace Teste_Back_end_Predify2.Controllers
{
    public class SupplierController : ApiController
    {
        private SupplierRepository supplierRepository;

        public SupplierController()
        {
            supplierRepository = new SupplierRepository();
        }

        public Task<List<SupplierDTO>> Get()
        {
            return supplierRepository.Get();
        }

        public IHttpActionResult Get(int id)
        {
            SupplierDTO supplier = supplierRepository.Get(id);

            if (supplier == null) return NotFound();

            return Ok(supplier);
        }

        [HttpGet]
        [Route("api/supplier/getbyname/{name}")]
        public async Task<List<SupplierDTO>> GetByName(string name)
        {
            List<SupplierDTO> suppliers = await supplierRepository.GetByName(name);
            return suppliers;
        }

        [HttpGet]
        [Route("api/supplier/getbycpfcnpj/{cpfcnpj}")]
        public async Task<List<SupplierDTO>> GetByCpfCnpj(string CpfCnpj)
        {
            List<SupplierDTO> suppliers = await supplierRepository.GetByCpfCnpj(CpfCnpj);
            return suppliers;
        }

        [HttpGet]
        [Route("api/supplier/getbydate")]
        public async Task<List<SupplierDTO>> GetByDate(DateFilterDTO dateFilter)
        {
            List<SupplierDTO> suppliers = await supplierRepository.GetByDate(dateFilter);
            return suppliers;
        }

        public IHttpActionResult Put(int id, Supplier Supplier)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != Supplier.Id) return BadRequest();

            return Ok(supplierRepository.Update(Supplier));
        }

        public IHttpActionResult Post(SupplierDTO supplierDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            SupplierDTO supplier = supplierRepository.Create(supplierDTO);

            if (supplier == null) return BadRequest();

            return Ok(supplier);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            Supplier Supplier = await supplierRepository.Delete(id);

            if (Supplier == null) return NotFound();

            return Ok(Supplier);
        }
    }
}
