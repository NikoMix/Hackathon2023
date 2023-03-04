// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.18.1

using Hackathon2023.Bots;
using Hackathon2023.Cache;
using Hackathon2023.Dialogs;
using Hackathon2023.Services.BotFramework;
using Hackathon2023.Services.Graph;
using Hackathon2023.Services.TeamsRecordingService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Graph.Communications.Common.Telemetry;

namespace Hackathon2023
{
    public class Startup
    {
        private readonly GraphLogger logger;
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            this.logger = new GraphLogger(typeof(Startup).Assembly.GetName().Name);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddOptions();

            services.AddHttpClient().AddControllers().AddNewtonsoftJson();
            services.AddHttpClient<ITeamsRecordingService, TeamsRecordingService>("TeamsRecordingService");
            services.AddSingleton<IGraphLogger>(this.logger);

            // Create the Bot Framework Authentication to be used with the Bot Adapter.
            services.AddSingleton<BotFrameworkAuthentication, ConfigurationBotFrameworkAuthentication>();

            // Create the Bot Adapter with error handling enabled.
            services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();

            // Create the storage we'll be using for User and Conversation state. (Memory is great for testing purposes.)
            services.AddSingleton<IStorage, MemoryStorage>();

            // Create the User state. (Used in this bot's Dialog implementation.)
            services.AddSingleton<UserState>();

            // Create the Conversation state. (Used by the Dialog system itself.)
            services.AddSingleton<ConversationState>();

            // Register LUIS recognizer
            services.AddSingleton<FlightBookingRecognizer>();

            // Register the BookingDialog.
            services.AddSingleton<BookingDialog>();

            // The MainDialog that will be run by the bot.
            services.AddSingleton<MainDialog>();

            // services.AddTransient<IBot, MessageBot>();
            services.AddTransient<CallingBot>();
            services.AddTransient<IBot, ActivityBot>();


            services.AddMicrosoftGraphServices(options => _configuration.Bind("AzureAd", options));
            // Create the bot as a transient. In this case the ASP Controller is expecting an IBot.
            services.AddTransient<IBot, DialogAndWelcomeBot<MainDialog>>();

            services.AddSingleton<IConnectorClientFactory, ConnectorClientFactory>();
            // services.AddScoped<ISpeechService, SpeechService>();

            services.AddTransient<IBotService, BotService>();
            services.AddCaches();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCookiePolicy();


            app.UseDefaultFiles()
                .UseStaticFiles()
                .UseWebSockets()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
