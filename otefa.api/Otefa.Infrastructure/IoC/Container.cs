namespace Otefa.Infrastructure.IoC
{
    public static class Container
    {

        static Container()
        {
            Current = new DummyContainer();
        }

        public static IContainer Current { get; set; }

    }
}