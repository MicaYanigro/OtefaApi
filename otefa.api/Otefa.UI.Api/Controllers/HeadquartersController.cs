﻿using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using Otefa.UI.Api.ViewModel.Team;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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


        [Route("name")]
        public Headquarter GetByName(string name)
        {
            return Headquarterservice.FindHeadquarterByName(name);
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post(HeadquarterViewModel HeadquarterViewModel)
        {
            try
            {
                var headquarter = Headquarterservice.Create(HeadquarterViewModel.Name, HeadquarterViewModel.Adress, HeadquarterViewModel.City);

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
        public HttpResponseMessage Put([FromUri] int headquarterID, [FromBody]PutHeadquarterViewModel PutHeadquarterViewModel)
        {
            try
            {
                Headquarterservice.Update(headquarterID, PutHeadquarterViewModel.Name, PutHeadquarterViewModel.Adress, PutHeadquarterViewModel.City);


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ExceptionBase e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }
        }

    }
}