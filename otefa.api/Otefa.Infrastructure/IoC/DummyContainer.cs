namespace Otefa.Infrastructure.IoC
{
    internal class DummyContainer : IContainer
    {

        public void Register<TInterface, TImplementation>(LifeCycle lf = LifeCycle.PerApplication) where TImplementation : TInterface
        {
        }

        public void Register<TInterface>(TInterface implementation)
        {
        }

        public TInterface Resolve<TInterface>()
        {
            return default(TInterface);
        }

        public TInterface ResolveOrDefault<TInterface>()
        {
            return default(TInterface);
        }

        public TInterface ResolveWithArguments<TInterface>(params object[] arguments)
        {
            return default(TInterface);
        }

        public TInterface Inject<TInterface>(TInterface toInject)
        {
            return toInject;
        }

    }
}