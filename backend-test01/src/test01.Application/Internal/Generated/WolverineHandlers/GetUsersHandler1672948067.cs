// <auto-generated/>
#pragma warning disable
using Microsoft.Extensions.Logging;
using RealPlaza.Core.Core.Persistence;

namespace Internal.Generated.WolverineHandlers
{
    // START: GetUsersHandler1672948067
    public class GetUsersHandler1672948067 : Wolverine.Runtime.Handlers.MessageHandler
    {
        private readonly RealPlaza.Core.Core.Persistence.IDbConnectionFactory _dbConnectionFactory1939779966;
        private readonly Microsoft.Extensions.Logging.ILogger<test01.Application.Users.Handlers.GetUsersHandler> _logger;

        public GetUsersHandler1672948067([Lamar.Named("NpgsqlConnectionFactory2")] RealPlaza.Core.Core.Persistence.IDbConnectionFactory dbConnectionFactory1939779966, Microsoft.Extensions.Logging.ILogger<test01.Application.Users.Handlers.GetUsersHandler> logger)
        {
            _dbConnectionFactory1939779966 = dbConnectionFactory1939779966;
            _logger = logger;
        }



        public override async System.Threading.Tasks.Task HandleAsync(Wolverine.Runtime.MessageContext context, System.Threading.CancellationToken cancellation)
        {
            var inline_userQueries = new test01.Persistence.UserRequests.Core.Queries.UserQueries(_dbConnectionFactory1939779966);
            var getUsersHandler = new test01.Application.Users.Handlers.GetUsersHandler(_logger, inline_userQueries);
            // The actual message body
            var getUsers = (test01.Internal.Contract.Users.Core.Commands.GetUsers)context.Envelope.Message;

            
            // The actual message execution
            var outgoing1 = await getUsersHandler.HandleAsync(getUsers, cancellation).ConfigureAwait(false);

            
            // Outgoing, cascaded message
            await context.EnqueueCascadingAsync(outgoing1).ConfigureAwait(false);

        }

    }

    // END: GetUsersHandler1672948067
    
    
}

