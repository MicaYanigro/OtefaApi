using Otefa.Domain.Model.Factories;
using Otefa.Domain.Model.Repositories;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using Otefa.Infrastructure.EmailSending;
using Otefa.Infrastructure.Persistence;

namespace Otefa.UI.Api
{
    internal sealed class Bootstrap
    {

        internal static void Initialize()
        {
            ConfigureIocContainer();
        }

        private static void ConfigureIocContainer()
        {
            Container.Current = new NinjectContainer();

            // Services
            Container.Current.Register<IPlayerService, PlayerService>();
            Container.Current.Register<ITeamService, TeamService>();
            Container.Current.Register<IHeadquarterService, HeadquarterService>();
            Container.Current.Register<ITournamentService, TournamentService>();
            Container.Current.Register<IMatchService, MatchService>();
            Container.Current.Register<IEmailSendingService, EmailSendingService>();
            Container.Current.Register<IEmailTemplateService, EmailTemplateService>();
            Container.Current.Register<ISmtpClientWrapper, SmtpClientWrapper>();
            Container.Current.Register<IFixtureGenerator, FixtureGenerator>();




            // Factories
            Container.Current.Register<IPlayerFactory, PlayerFactory>();
            Container.Current.Register<ITeamFactory, TeamFactory>();
            Container.Current.Register<ITournamentFactory, TournamentFactory>();
            Container.Current.Register<IHeadquarterFactory, HeadquarterFactory>();
            Container.Current.Register<IMatchFactory, MatchFactory>();
            Container.Current.Register<IPlayerDetailsFactory, PlayerDetailsFactory>();

            IRepositoryContext repositoryContext = new RepositoryContextEF();
            Container.Current.Register(repositoryContext);

            Container.Current.Register<IRepositoryContext, RepositoryContextEF>(LifeCycle.PerRequest);
            Container.Current.Register<IPlayerRepository, PlayerRepositoryEF>(LifeCycle.PerRequest);
            Container.Current.Register<ITeamRepository, TeamRepositoryEF>(LifeCycle.PerRequest);
            Container.Current.Register<ITournamentRepository, TournamentRepositoryEF>(LifeCycle.PerRequest);
            Container.Current.Register<IHeadquarterRepository, HeadquarterRepositoryEF>(LifeCycle.PerRequest);
            Container.Current.Register<IMatchRepository, MatchRepositoryEF>(LifeCycle.PerRequest);
            Container.Current.Register<IMatchTeamRepository, MatchTeamRepositoryEF>(LifeCycle.PerRequest);
        }

    }
}