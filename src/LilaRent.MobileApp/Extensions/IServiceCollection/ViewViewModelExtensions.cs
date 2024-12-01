using LilaRent.MobileApp.Services;


namespace LilaRent.MobileApp.Extensions;


public static class ViewViewModelExtensions
{
    private static int s_cashedDescriptorIndex = 0;


    public static IServiceCollection AddWithVVmResolving(
        this IServiceCollection @this,
        IServiceDescriptor<IView, IView> viewDescriptor,
        IServiceDescriptor<IViewModel, IViewModel> viewModelDescriptor)
    {
        @this.Add(viewDescriptor.ToServiceDescriptor());
        @this.Add(viewModelDescriptor.ToServiceDescriptor());

        AddToVVmResolveService(@this, viewDescriptor.ServiceType, viewModelDescriptor.ServiceType);

        return @this;
    }


    private static void AddToVVmResolveService(IServiceCollection services, Type viewType, Type viewModelType)
    {
        int descriptorIndex;

        if (services[s_cashedDescriptorIndex].ServiceType == typeof(IVVmResolveService))
        {
            descriptorIndex = s_cashedDescriptorIndex;
        }
        else
        {
            descriptorIndex = services
                .Select((descriptor, index) => (Descriptor: descriptor, Index: index))
                .Where(di => di.Descriptor.ServiceType == typeof(IVVmResolveService))
                .Select(di => di.Index)
                .DefaultIfEmpty(-1)
                .First();

            s_cashedDescriptorIndex = descriptorIndex;
        }

        Func<IServiceProvider, VVmResolveService> factory;

        if (descriptorIndex == -1)
        {
            factory = serviceProvider =>
            {
                return new VVmResolveService(serviceProvider, new VVmTypePair(viewType, viewModelType));
            };
        }
        else if (services[descriptorIndex]?.ImplementationFactory is var oldFactory)
        {
            factory = serviceProvider =>
            {
                var service = (VVmResolveService)oldFactory!(serviceProvider);
                service.Add(viewType, viewModelType);
                return service;
            };
        }
        else
        {
            throw new InvalidOperationException("Can not add new View ViewModel pair because impossible to get factory.");
        }

        var descriptor = ServiceDescriptor.Singleton<IVVmResolveService, VVmResolveService>(factory);

        if (descriptorIndex == -1)
        {
            services.Add(descriptor);
            s_cashedDescriptorIndex = 0;
        }
        else
        {
            services[descriptorIndex] = descriptor;
        }
    }
}


public interface IServiceDescriptor<out TService, out TImplementation> 
    where TService : class 
    where TImplementation : TService
{
    ServiceLifetime Lifetime { get; }

    Type ServiceType => typeof(TService);
    Type ImplementationType => typeof(TImplementation);

    TImplementation? ImplementationInstance { get; }
    Func<IServiceProvider, TImplementation>? ImplementationFactory { get; }
}

public static class IServiceDescriptorExtension
{
    public static ServiceDescriptor ToServiceDescriptor<TService, TImplementation>(this IServiceDescriptor<TService, TImplementation> @this)
        where TService : class
        where TImplementation : TService
    {
        if (@this.ImplementationInstance is not null)
        {
            return new ServiceDescriptor(@this.ServiceType, @this.ImplementationInstance);
        }
        else if (@this.ImplementationFactory is not null)
        {
            var serviceType = @this.ServiceType;
            var lifetime = @this.Lifetime;
            Func<IServiceProvider, object> implementationFactory = serviceProvider => @this.ImplementationFactory(serviceProvider);

            return new ServiceDescriptor(serviceType, implementationFactory, lifetime);
        }
        else
        {
            var serviceType = @this.ServiceType;
            var implementationType = @this.ImplementationType;
            var lifetime = @this.Lifetime;

            return new ServiceDescriptor(serviceType, implementationType, lifetime);
        }
    }
}


public class ServiceDescriptor<TService, TImplementation> : IServiceDescriptor<TService, TImplementation> 
    where TService : class 
    where TImplementation : TService
{
    public ServiceLifetime Lifetime { get; }

    public TImplementation? ImplementationInstance { get; }
    public Func<IServiceProvider, TImplementation>? ImplementationFactory { get; }


    public ServiceDescriptor(ServiceLifetime lifetime)
    {
        Lifetime = lifetime;

        ImplementationInstance = default;
        ImplementationFactory = null;
    }

    public ServiceDescriptor(TImplementation implementationInstance)
    {
        Lifetime = ServiceLifetime.Singleton;

        ImplementationInstance = implementationInstance;
        ImplementationFactory = null;
    }

    public ServiceDescriptor(ServiceLifetime lifetime, Func<IServiceProvider, TImplementation> implementationFactory)
    {
        Lifetime = lifetime;

        ImplementationInstance = default;
        ImplementationFactory = implementationFactory;
    }


    public Type ServiceType => typeof(TService);
    public Type ImplementationType => typeof(TImplementation);


    public static implicit operator ServiceDescriptor(ServiceDescriptor<TService, TImplementation> typedDescriptor)
    {
        return typedDescriptor.ToServiceDescriptor();
    }
}


public class ServiceFactory<TBaseService> where TBaseService : class
{
    public static ServiceDescriptor<TService, TService> Singleton<TService>()
        where TService : class, TBaseService
    {
        return new ServiceDescriptor<TService, TService>(ServiceLifetime.Singleton);
    }

    public static ServiceDescriptor<TService, TImplementation> Singleton<TService, TImplementation>()
        where TService : class, TBaseService
        where TImplementation : TService
    {
        return new ServiceDescriptor<TService, TImplementation>(ServiceLifetime.Singleton);
    }

    public static ServiceDescriptor<TService, TService> Singleton<TService>(TService implementationInstance)
        where TService : class, TBaseService
    {
        return new ServiceDescriptor<TService, TService>(implementationInstance);
    }

    public static ServiceDescriptor<TService, TService> Singleton<TService>(Func<IServiceProvider, TService> implementationFactory)
        where TService : class, TBaseService
    {
        return new ServiceDescriptor<TService, TService>(ServiceLifetime.Singleton, implementationFactory);
    }


    public static ServiceDescriptor<TService, TService> Transient<TService>()
        where TService : class, TBaseService
    {
        return new ServiceDescriptor<TService, TService>(ServiceLifetime.Transient);
    }
}

public class ViewServiceFactory : ServiceFactory<IView>
{ }

public class ViewModelServiceFactory : ServiceFactory<IViewModel>
{ }
