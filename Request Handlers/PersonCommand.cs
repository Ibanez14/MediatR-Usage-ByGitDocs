using MediatR;
using MediatR.Pipeline;
using MediatR___CQRS_Testing.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR2._2WebApplication1.V1.CQ_Models
{
    // 
    // Here we use Request, Handlers and Pipeline behaviors
    //

    /// <summary>
    /// Create a command and return bool
    /// </summary>
    public class PersonCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }



    // Handler
    /// <summary>
    /// Handler that take PersonCommand and return bool
    /// </summary>
    public class PersonCommandHandler : IRequestHandler<PersonCommand, bool>
    {
        readonly DataContext context;
        public PersonCommandHandler(DataContext _context)
        {
            context = _context;
        }

        public async Task<bool> Handle(PersonCommand request, CancellationToken cancellationToken)
        {
            context.Set<Person>().Add(new Person()
            {
                Age = request.Age,
                Name = request.Name
            });
            int affected = await context.SaveChangesAsync();
            return affected > 0;
        }
    }


    // PipeLine Behavior .. Called in order registered in Startup

    public class BehaviorOf_PersonCommandHandler : IPipelineBehavior<PersonCommand, bool>
    {
        // You can inject any service
        public async Task<bool> Handle(PersonCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<bool> next)
        {
            request.Age += 100;

            // PersonCommandHandler or next PipeLineBehavior called behind the scenes
            bool result = await next();

            return result;
        }
    }





    public class BehaviorOf_PersonCommandHandler2 : IPipelineBehavior<PersonCommand, bool>
    {
        // You can inject any service
        public async Task<bool> Handle(PersonCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<bool> next)
        {
            // some logic;
            await next();

            return true;
        }
    }
    


    // Generic Pipeline, is called usually before any handler

    public class LogginBehavior<T,B> : IPipelineBehavior<T, B>
    {
        private readonly ILogger<LogginBehavior<T, B>> logger;

        // You can inject any service

        public LogginBehavior(ILogger<LogginBehavior<T,B>> logger)
        {
            this.logger = logger;
        }

        public async Task<B> Handle(T request, 
                      CancellationToken cancellationToken,
                     RequestHandlerDelegate<B> next)
        {
            logger.LogInformation($"Request is :{typeof(T).Name}");
             await next();

            return default;
        }
    }

}
