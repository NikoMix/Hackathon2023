using Microsoft.Bot.Connector;

namespace Hackathon2023.Services.BotFramework;

/// <summary>
/// Factory to for handling a Bot Framework connector client
/// </summary>
public interface IConnectorClientFactory
{
    /// <summary>
    /// Creates a connector client based on the service url.
    /// Ensures only one client per serviceUrl/appId/scope is created
    /// </summary>
    /// <returns>The connector client</returns>
    ConnectorClient CreateConnectorClient();
}