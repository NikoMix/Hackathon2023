namespace Hackathon2023.Controllers
{
    /// <summary>
    /// HTTP route constants for routing requests to CallController methods.
    /// </summary>
    public static class HttpRouteConstants
    {
        /// <summary>
        /// Route prefix for all incoming requests.
        /// </summary>
        public const string CallSignalingRoutePrefix = "api/calling";

        /// <summary>
        /// Route prefix for all incoming requests.
        /// </summary>
        public const string CallbackPrefix = "/callback";

        /// <summary>
        /// Route for incoming requests including notifications, callbacks and incoming call.
        /// </summary>
        public const string OnIncomingRequestRoute = CallbackPrefix + "/calling";

        /// <summary>
        /// Route for join call request.
        /// </summary>
        public const string OnJoinCallRoute = "/joinCall";

        /// <summary>
        /// Route for making outgoing call request.
        /// </summary>
        public const string OnMakeCallRoute = "/makeCall";



        public const string ParticipantsCalling = "participantscalling/raise";

        /// <summary>
        /// The calls suffix.
        /// </summary>
        public const string CallsPrefix = "/calls";

        /// <summary>
        /// Route for getting Image for a call.
        /// </summary>
        public const string CallRoutePrefix = CallsPrefix + "/{callLegId}";

        /// <summary>
        /// Route for adding participants request.
        /// </summary>
        public const string OnAddParticipantRoute = CallRoutePrefix + "/addParticipant";

        /// <summary>
        /// Route for subscribe to tone request.
        /// </summary>
        public const string OnSubscribeToToneRoute = CallRoutePrefix + "/subscribeToTone";


        /// <summary>
        /// Route for incoming notification requests.
        /// </summary>
        public const string OnNotificationRequestRoute = "notification";

        /// <summary>
        /// The logs route for GET.
        /// </summary>
        public const string Logs = "logs";

        /// <summary>
        /// The calls route for both GET and POST.
        /// </summary>
        public const string Calls = "calls";

        /// <summary>
        /// The route for join call.
        /// </summary>
        public const string JoinCall = "joinCall";

        /// <summary>
        /// The route for getting the call.
        /// </summary>
        public const string CallRoute = Calls + "/{callLegId}";

        /// <summary>
        /// Route for changing screen sharing role request.
        /// </summary>
        public const string OnChangeRoleRoute = "changeRole";

        /// <summary>
        /// Route for health check request.
        /// </summary>
        public const string HealthRoute = "health";
    }
}
