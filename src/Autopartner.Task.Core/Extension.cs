using Autopartner.Task.Core.Common.Validation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Autopartner.Task.Core;

public static class Extension
{
    public static void AddCore(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Extension).Assembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(typeof(Extension).Assembly);
    }
}
