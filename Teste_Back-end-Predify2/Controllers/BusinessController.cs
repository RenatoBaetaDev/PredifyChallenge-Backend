using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Teste_Back_end_Predify2.Models;
using Teste_Back_end_Predify2.Repositories;
using System.Threading.Tasks;

namespace Teste_Back_end_Predify2.Controllers
{
    public class BusinessController : ApiController
    {
        private BusinessRepository businessRepository;

        public BusinessController()
        {
            businessRepository = new BusinessRepository();
        }

        public Task<List<BusinessDTO>> Get()
        {
            return businessRepository.Get();
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            BusinessDTO business = await businessRepository.Get(id);

            if (business == null) return NotFound();

            return Ok(business);
        }

        public IHttpActionResult Post(BusinessDTO businessDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(businessRepository.Create(businessDTO));
        }

        public IHttpActionResult Put(int id, Business business)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != business.Id) return BadRequest();

            return Ok(businessRepository.Update(business));
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            Business business = await businessRepository.Delete(id);

            if (business == null) return NotFound();

            return Ok(business);
        }
    }
}
