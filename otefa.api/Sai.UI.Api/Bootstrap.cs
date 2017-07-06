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
            Container.Current.Register<IEmailSendingService, EmailSendingService>();
            Container.Current.Register<IEmailTemplateService, EmailTemplateService>();
            Container.Current.Register<ISmtpClientWrapper, SmtpClientWrapper>();
        

            // Factories
            Container.Current.Register<IPlayerFactory, PlayerFactory>();

            IRepositoryContext repositoryContext = new RepositoryContextEF();
            Container.Current.Register(repositoryContext);
            
            Container.Current.Register<IRepositoryContext, RepositoryContextEF>(LifeCycle.PerRequest);
        
        }

    }
}