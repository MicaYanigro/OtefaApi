using NUnit.Framework;
using System;

namespace Sai.Infrastructure.IoC.IntegrationTesting
{
    [TestFixture]
    public class NinjectContainerTests
    {

        [Test]
        public void ARegisteredImplementationCanBeResolved()
        {
            NinjectContainer container = new NinjectContainer();

            container.Register<IAnInterface, AClass>();

            IAnInterface resolved = container.Resolve<IAnInterface>();

            Assert.That(resolved, Is.InstanceOf<AClass>());
        }

        [Test]
        public void AnUnregisteredImplementationCannotBeResolved()
        {
            NinjectContainer container = new NinjectContainer();

            Exception ex = Assert.Catch(() => container.Resolve<IAnInterface>());

            Assert.That(ex, Is.InstanceOf<NoRegisteredImplementationException>());
        }

        [Test]
        public void AConcreteImplementationCanBeResolved()
        {
            NinjectContainer container = new NinjectContainer();

            AClass implementation = new AClass();

            container.Register<IAnInterface>(implementation);

            IAnInterface resolved = container.Resolve<IAnInterface>();

            Assert.That(resolved, Is.SameAs(implementation));
        }

        [Test]
        public void ARegisteredImplementationCanBeResolvedAsDefault()
        {
            NinjectContainer container = new NinjectContainer();

            container.Register<IAnInterface, AClass>();

            IAnInterface resolved = container.ResolveOrDefault<IAnInterface>();

            Assert.That(resolved, Is.Not.Null);
        }

        [Test]
        public void AnUnregisteredImplementationCanBeResolvedAsDefault()
        {
            NinjectContainer container = new NinjectContainer();

            IAnInterface resolved = container.ResolveOrDefault<IAnInterface>();

            Assert.That(resolved, Is.Null);
        }

        [Test]
        public void ARegisteredImplementationCanBeResolvedWithConstructorArguments()
        {
            NinjectContainer container = new NinjectContainer();

            container.Register<IAnInterfaceWithAProperty, AClassWithAProperty>();

            IAnInterfaceWithAProperty instance = container.ResolveWithArguments<IAnInterfaceWithAProperty>(8);

            Assert.That(instance, Is.InstanceOf<AClassWithAProperty>());
            Assert.That(instance.TheProperty, Is.EqualTo(8));
        }

        [Test]
        public void AnUnregisteredImplementationCannotBeResolvedEvenWithConstructorArguments()
        {
            NinjectContainer container = new NinjectContainer();

            Exception ex = Assert.Catch(() => container.ResolveWithArguments<IAnInterfaceWithAProperty>(9));

            Assert.That(ex, Is.InstanceOf<NoRegisteredImplementationException>());
        }

        [Test]
        public void AnInstanceIsInjected()
        {
            NinjectContainer container = new NinjectContainer();

            container.Register<IAnInterface, AClass>();

            AClassWithAnInjectableProperty instance = container.Inject(new AClassWithAnInjectableProperty());

            Assert.That(instance, Is.Not.Null);
            Assert.That(instance.TheInjectableProperty, Is.Not.Null);
        }

        [Test]
        public void AnInstanceIsAutoInjected()
        {
            NinjectContainer container = new NinjectContainer();

            container.Register<IAnInterfaceWithAnInjectableProperty, AClassWithAnInjectableProperty>();
            container.Register<IAnInterface, AClass>();

            IAnInterfaceWithAnInjectableProperty instance = container.Resolve<IAnInterfaceWithAnInjectableProperty>();

            Assert.That(instance, Is.Not.Null);
            Assert.That(instance.TheInjectableProperty, Is.Not.Null);
        }
    }

    interface IAnInterface
    {
    }
    class AClass : IAnInterface
    {
    }

    interface IAnInterfaceWithAProperty
    {
        int TheProperty { get; set; }
    }

    class AClassWithAProperty : IAnInterfaceWithAProperty
    {

        public AClassWithAProperty(int thePropertyValue)
        {
            TheProperty = thePropertyValue;
        }

        public int TheProperty { get; set; }

    }

    interface IAnInterfaceWithAnInjectableProperty
    {
        IAnInterface TheInjectableProperty { get; set; }
    }

    class AClassWithAnInjectableProperty : IAnInterfaceWithAnInjectableProperty
    {
        [Injectable]
        public IAnInterface TheInjectableProperty { get; set; }
    }

}