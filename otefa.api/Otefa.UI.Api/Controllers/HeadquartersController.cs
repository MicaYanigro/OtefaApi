using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using Otefa.UI.Api.ViewModel.Headquarter;
using Otefa.UI.Api.ViewModel.Team;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Otefa.UI.Api.Controllers
{
    [RoutePrefix("v1/headquarters")]
    public class HeadquartersController : ApiControllerBase
    {

        public HeadquartersController()
        {
        }

        [Injectable]
        public IHeadquarterService Headquarterservice { get; set; }

        public HeadquartersController(IHeadquarterService Headquarterservice)
        {
            this.Headquarterservice = Headquarterservice;
        }


        [Route("get/{name}")]
        public Headquarter GetByName(string name)
        {
            return Headquarterservice.FindHeadquarterByName(name);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Post(HeadquarterViewModel HeadquarterViewModel)
        {
            try
            {
                var headquarter = await Headquarterservice.Create(HeadquarterViewModel.Name, HeadquarterViewModel.address, HeadquarterViewModel.City);

                return Request.CreateResponse(HttpStatusCode.Created, headquarter.GetId());
            }
            catch (ExceptionBase e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }
        }

        [Route("")]
        public IEnumerable<Headquarter> Get()
        {
            return Headquarterservice.GetAll();
        }

        [HttpPut]
        [Route("{headquarterID}")]
        public async Task<HttpResponseMessage> Put([FromUri] int headquarterID, [FromBody]PutHeadquarterViewModel PutHeadquarterViewModel)
        {
            try
            {
                await Headquarterservice.Update(headquarterID, PutHeadquarterViewModel.Name, PutHeadquarterViewModel.address, PutHeadquarterViewModel.City);


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ExceptionBase e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }
        }

    }
}