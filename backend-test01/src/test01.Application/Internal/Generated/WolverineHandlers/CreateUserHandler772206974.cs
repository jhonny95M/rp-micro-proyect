// <auto-generated/>
#pragma warning disable
using Lamar;

namespace Internal.Generated.WolverineHandlers
{
    // START: CreateUserHandler772206974
    public class CreateUserHandler772206974 : Wolverine.Runtime.Handlers.MessageHandler
    {
        private readonly Lamar.IContainer _rootContainer;

        public CreateUserHandler772206974(Lamar.IContainer rootContainer)
        {
            _rootContainer = rootContainer;
        }



        public override async System.Threading.Tasks.Task HandleAsync(Wolverine.Runtime.MessageContext context, System.Threading.CancellationToken cancellation)
        {
            await using var nestedContainer = (Lamar.IContainer)_rootContainer.GetNestedContainer();
            
            /*
            * Dependency: new UnitOfWork(serviceProvider, userRepository)
            * 
            * Dependency: new UserRepository(transaction)
            * 
            * Dependency: new GenericTransaction(connection)
            * 
            * Dependency: Lambda Factory of NpgsqlConnection
            * The scoping is Scoped, so a Lambda registration requires
            * the usage of a nested container for resolution for correct scoping.
            * A formal factory provider may be an alternative
            * 
            * 
            * Dependency: Lambda Factory of NpgsqlConnection
            * The scoping is Scoped, so a Lambda registration requires
            * the usage of a nested container for resolution for correct scoping.
            * A formal factory provider may be an alternative
            * 
            * 
            * Dependency: new GenericTransaction(connection)
            * 
            * Dependency: Lambda Factory of NpgsqlConnection
            * The scoping is Scoped, so a Lambda registration requires
            * the usage of a nested container for resolution for correct scoping.
            * A formal factory provider may be an alternative
            * 
            * 
            * Dependency: Lambda Factory of NpgsqlConnection
            * The scoping is Scoped, so a Lambda registration requires
            * the usage of a nested container for resolution for correct scoping.
            * A formal factory provider may be an alternative
            * 
            * 
            * Dependency: new UserRepository(transaction)
            * 
            * Dependency: new GenericTransaction(connection)
            * 
            * Dependency: Lambda Factory of NpgsqlConnection
            * The scoping is Scoped, so a Lambda registration requires
            * the usage of a nested container for resolution for correct scoping.
            * A formal factory provider may be an alternative
            * 
            * 
            * Dependency: Lambda Factory of NpgsqlConnection
            * The scoping is Scoped, so a Lambda registration requires
            * the usage of a nested container for resolution for correct scoping.
            * A formal factory provider may be an alternative
            * 
            * 
            * Dependency: new GenericTransaction(connection)
            * 
            * Dependency: Lambda Factory of NpgsqlConnection
            * The scoping is Scoped, so a Lambda registration requires
            * the usage of a nested container for resolution for correct scoping.
            * A formal factory provider may be an alternative
            * 
            * 
            * Dependency: Lambda Factory of NpgsqlConnection
            * The scoping is Scoped, so a Lambda registration requires
            * the usage of a nested container for resolution for correct scoping.
            * A formal factory provider may be an alternative
            */
            var createUserHandler = nestedContainer.GetInstance<test01.Application.Users.Handlers.CreateUserHandler>();
            // The actual message body
            var createUser = (test01.Internal.Contract.Users.Core.Commands.CreateUser)context.Envelope.Message;

            
            // The actual message execution
            var outgoing1 = await createUserHandler.HandleAsync(createUser, cancellation).ConfigureAwait(false);

            
            // Outgoing, cascaded message
            await context.EnqueueCascadingAsync(outgoing1).ConfigureAwait(false);

        }

    }

    // END: CreateUserHandler772206974
    
    
}

