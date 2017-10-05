using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using Otefa.UI.Api.ViewModel.New;
using Otefa.UI.Api.ViewModel.Team;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Otefa.UI.Api.Controllers
{
    [RoutePrefix("v1/news")]
    public class NewsController : ApiControllerBase
    {

        public NewsController()
        {
        }

        [Injectable]
        public INewService NewService { get; set; }

        public NewsController(INewService newservice)
        {
            this.NewService = newservice;
        }
        

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post(NewViewModel newViewModel)
        {
            try
            {
                var newObject = NewService.Create(newViewModel.Date, newViewModel.Title, newViewModel.Body, newViewModel.Image);

                return Request.CreateResponse(HttpStatusCode.Created, newObject.GetId());
            }
            catch (ExceptionBase e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }
        }

        [Route("")]
        public IEnumerable<New> Get()
        {
            return NewService.GetAll();
        }

        [HttpPut]
        [Route("{newID}")]
        public HttpResponseMessage Put([FromUri] int newID, [FromBody]PutNewViewModel PutnewViewModel)
        {
            try
            {
                NewService.Update(newID, PutnewViewModel.Date, PutnewViewModel.Title, PutnewViewModel.Body, PutnewViewModel.Image);


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ExceptionBase e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }
        }


        [HttpDelete]
        [Route("{newID}")]
        public HttpResponseMessage Delete([FromUri] int newID)
        {
            try
            {
                NewService.Delete(newID);


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ExceptionBase e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }
        }

        [HttpPut]
        [Route("{newID}/activate")]
        public HttpResponseMessage Activate([FromUri] int newID)
        {
            try
            {
                NewService.Activate(newID);
                
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ExceptionBase e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }
        }

    }
}