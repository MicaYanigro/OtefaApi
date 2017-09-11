using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using Otefa.UI.Api.ViewModel.Team;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Otefa.UI.Api.Controllers
{
    [RoutePrefix("v1/teams")]
    public class TeamsController : ApiControllerBase
    {

        public TeamsController()
        {
        }

        [Injectable]
        public ITeamService Teamservice { get; set; }

        public TeamsController(ITeamService Teamservice)
        {
            this.Teamservice = Teamservice;
        }

        [Route("{id}")]
        public Team GetByID(int id)
        {
            return Teamservice.GetByID(id);
        }

        [Route("Get/{name}")]
        public Team GetByName(string name)
        {
            return Teamservice.FindTeamByName(name);
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post(TeamViewModel TeamViewModel)
        {
            try
            {
                var team = Teamservice.Create(TeamViewModel.Name, TeamViewModel.TeamDelegate, TeamViewModel.ShieldImage,
                                 TeamViewModel.TeamImage, TeamViewModel.PlayersList);

                return Request.CreateResponse(HttpStatusCode.Created, team.GetId());
            }
            catch (ExceptionBase e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }
        }
        
        [Route("")]
        public IEnumerable<Team> Get()
        {
            return Teamservice.GetAll();
        }

        [Route("{teamID}/stadistics")]
        public IEnumerable<ExpandoObject> GetStadistics([FromUri] int teamID)
        {
            return Teamservice.GetTeamStadistics(teamID);
        }

        [Route("{teamID}/historicalStadistics")]
        public ExpandoObject HistoricalStadistics([FromUri] int teamID)
        {
            return Teamservice.GetHistoricalStadistics(teamID);
        }

        [Route("{teamID}/upcomingMatches")]
        public IEnumerable<Match> UpcomingMatches([FromUri] int teamID)
        {
            return Teamservice.GetUpcomingMatches(teamID);
        }

        [HttpPut]
        [Route("{teamID}")]
        public HttpResponseMessage Put([FromUri] int teamID, [FromBody]PutTeamViewModel PutTeamViewModel)
        {
            try
            {
                Teamservice.Update(teamID, PutTeamViewModel.Name, PutTeamViewModel.TeamDelegate, PutTeamViewModel.ShieldImage,
                                    PutTeamViewModel.TeamImage, PutTeamViewModel.PlayersList);


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ExceptionBase e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }
        }

    }
}