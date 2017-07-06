using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Web.Common;

namespace Otefa.Infrastructure.IoC
{
    public sealed class NinjectContainer : IContainer
    {

        private KernelBase kernel;

        public NinjectContainer()
        {
            kernel = new StandardKernel(new NinjectSettings()
            {
                InjectAttribute = typeof(InjectableAttribute)
            });
        }

        public void Register<TInterface, TImplementation>(LifeCycle lf = LifeCycle.PerApplication) where TImplementation : TInterface
        {
            kernel.Unbind<TInterface>();
            var bindConfig = kernel.Bind<TInterface>().To<TImplementation>();
            if (lf == LifeCycle.PerRequest)
            {
                bindConfig.InRequestScope();
            }
        }

        public void Register<TInterface>(TInterface implementation)
        {
            kernel.Unbind<TInterface>();
            kernel.Bind<TInterface>().ToConstant<TInterface>(implementation);
        }

        public TInterface Resolve<TInterface>()
        {
            try
            {
                return kernel.Get<TInterface>();
            }
            catch (ActivationException)
            {
                throw new NoRegisteredImplementationException();
            }
        }

        public TInterface ResolveOrDefault<TInterface>()
        {
            try
            {
                return Resolve<TInterface>();
            }
            catch (NoRegisteredImplementationException)
            {
                return default(TInterface);
            }
        }

        public TInterface ResolveWithArguments<TInterface>(params object[] arguments)
        {
            try
            {

                object kernelTarget = kernel.GetBindings(typeof(TInterface)).First().ProviderCallback.Target;

                Type implementationType = (Type)kernelTarget.GetType().GetField("prototype").GetValue(kernelTarget);

                var constructor = implementationType.GetConstructor(arguments.Select(arg => arg.GetType()).ToArray());

                List<ConstructorArgument> constructorArguments = new List<ConstructorArgument>();

                int i = 0;

                foreach (var parameter in constructor.GetParameters())
                {
                    constructorArguments.Add(new ConstructorArgument(parameter.Name, arguments[i++]));
                }

                return kernel.Get<TInterface>(constructorArguments.ToArray());

            }
            catch
            {
                throw new NoRegisteredImplementationException();
            }
        }

        public TInterface Inject<TInterface>(TInterface toInject)
        {
            kernel.Inject(toInject);

            return toInject;
        }

    }
}