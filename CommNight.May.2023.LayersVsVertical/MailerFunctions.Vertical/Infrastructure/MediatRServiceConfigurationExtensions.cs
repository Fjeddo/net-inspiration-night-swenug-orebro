using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace MailerFunctions.Vertical.Infrastructure;

public static class MediatRServiceConfigurationExtensions
{
    public static MediatRServiceConfiguration AddValidator(this MediatRServiceConfiguration configuration, Type handlerType) 
        => configuration.AddBehavior(handlerType.GetInterfaces().Single(), handlerType);

    public static MediatRServiceConfiguration AddValidator<THandler>(this MediatRServiceConfiguration configuration)
        => configuration.AddValidator(typeof(THandler));
}
