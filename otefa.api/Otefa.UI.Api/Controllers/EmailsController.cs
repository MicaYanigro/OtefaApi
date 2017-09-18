using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using Otefa.UI.Api.ViewModel.Emails;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Otefa.UI.Api.Controllers
{
    [RoutePrefix("v1/emails")]
    public class EmailsController : ApiControllerBase
    {

        [Injectable]
        public IEmailSendingService EmailSendingService { get; set; }

        [Route("")]
        public HttpResponseMessage Post([FromBody] EmailViewModel emailViewModel)
        {
            try
            {
                EmailSendingService.Send(emailViewModel.Body, emailViewModel.ReplyTo);

                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (ArgumentException e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("Email could not be sent"));
            }
        }

    }
}