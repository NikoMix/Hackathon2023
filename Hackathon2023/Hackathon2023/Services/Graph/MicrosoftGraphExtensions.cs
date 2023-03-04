namespace Hackathon2023.Services.Graph;

using System;
using Azure.Identity;
using Hackathon2023.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;


public static class MicrosoftGraphExtensions
{
    /// <summary>
    /// Adds the services that are available in this project to Dependency Injection.
    /// Include this in your Startup.cs ConfigureServices if you need to access these services.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="azureAdOptionsAction">AzureAD Options.</param>
    /// <returns>Service collections.</returns>
    public static IServiceCollection AddMicrosoftGraphServices(this IServiceCollection services, Action<AzureAdOptions> azureAdOptionsAction)
    {
        var options = new AzureAdOptions();
        azureAdOptionsAction(options);

        ClientSecretCredential authenticationProvider = new ClientSecretCredential(options.TenantId, options.ClientId, options.ClientSecret);

        services.AddScoped<GraphServiceClient, GraphServiceClient>(sp =>
        {
            return new GraphServiceClient(authenticationProvider);
        });

        services.AddTransient<ICallService, CallService>();
        services.AddTransient<IChatService, ChatService>();
        services.AddTransient<IOnlineMeetingService, OnlineMeetingService>();
        services.AddSingleton<AudioRecordingConstants>();
        return services;
    }
}
