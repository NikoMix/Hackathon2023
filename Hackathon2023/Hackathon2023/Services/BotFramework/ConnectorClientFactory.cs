using Hackathon2023.Options;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Connector;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System;
using System.Net.Http;

namespace Hackathon2023.Services.BotFramework;

/// <inheritdoc/>
public class ConnectorClientFactory : IConnectorClientFactory
{
    private readonly BotOptions botOptions;
    private readonly ConcurrentDictionary<string, ConnectorClient> connectorClients = new ConcurrentDictionary<string, ConnectorClient>();
    private readonly ILogger<ConnectorClientFactory> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConnectorClientFactory "/> class.
    /// </summary>
    /// <param name="logger">Logger.</param>
    public ConnectorClientFactory(IOptions<BotOptions> botOptions, ILogger<ConnectorClientFactory> logger)
    {
        this.botOptions = botOptions.Value;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public ConnectorClient CreateConnectorClient()
    {
        return CreateConnectorClient(
            // We are using the Global Service URL endpoint which will work if you only call public clouds. If you need to support government clouds,
            // you will need to keep track of the serviceUrl used in MessageBot handler, and pass it here.
            // You can learn more about this in the documentation: https://learn.microsoft.com/en-us/microsoftteams/platform/bots/how-to/conversations/send-proactive-messages?tabs=dotnet#create-the-conversation
            new Uri("https://smba.trafficmanager.net/teams/"),
            new MicrosoftAppCredentials(botOptions.AppId, botOptions.AppSecret));
    }

    private ConnectorClient CreateConnectorClient(Uri serviceUrl, AppCredentials appCredentials)
    {
        // As multiple bots can listen on a single serviceUrl, the clientKey also includes the OAuthScope.
        var clientKey = $"{serviceUrl}:{appCredentials?.MicrosoftAppId}:{appCredentials?.OAuthScope}";

        return connectorClients.GetOrAdd(clientKey, (key) =>
        {
            return new ConnectorClient(serviceUrl, appCredentials, new HttpClient());
        });
    }
}