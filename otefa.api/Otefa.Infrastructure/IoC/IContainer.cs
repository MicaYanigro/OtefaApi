namespace Otefa.Infrastructure.IoC
{
    public interface IContainer
    {
        void Register<TInterface, TImplementation>(LifeCycle lf = LifeCycle.PerApplication) where TImplementation : TInterface;

        void Register<TInterface>(TInterface implementation);

        TInterface Resolve<TInterface>();

        TInterface ResolveOrDefault<TInterface>();

        TInterface ResolveWithArguments<TInterface>(params object[] arguments);

        TInterface Inject<TInterface>(TInterface toInject);
    }
}