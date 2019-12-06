using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR___CQRS_Testing.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MediatR.Pipeline;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MediatR___CQRS_Testing.Controllers
{


    //
    // Here we use Requests, Handler, and Pre with Post processors
    //

    /// <summary>
    /// First create query message and specify return result
    /// </summary>
    public class PersonQuery : IRequest<PersonResponse>
    {
        public string Name { get; set; }
    }




    /// <summary>
    /// Handler that takes query ... maps it to Person...Save in DV....return response
    /// </summary>
    public class PersonQueryHandler : IRequestHandler<PersonQuery, PersonResponse>
    {
        DataContext context;
        public PersonQueryHandler(DataContext _context)
        {
            context = _context;
        }


        public async Task<PersonResponse> Handle(PersonQuery request,
                                          CancellationToken cancellationToken)
        {
            var person = await context.Set<Person>().FirstOrDefaultAsync(p => p.Name == request.Name);

            var response = new PersonResponse()
            {
                FullInfo = $"Name: {person?.Name}, Age: {person?.Age}"
            };
            return response;
        }

    }


    #region Second Handler that is not called


    // This is handler for the same models but only one of these handlers will be called

    //public class PersonQueryHandler2 : IRequestHandler<PersonQuery, PersonResponse>
    //{
    //    private readonly ILogger<PersonQuery> logger;

    //    public PersonQueryHandler2(ILogger<PersonQuery> logger)
    //    {
    //        this.logger = logger;
    //    }

    //    public Task<PersonResponse> Handle(PersonQuery request, CancellationToken cancellationToken)
    //    {
    //        logger.LogInformation("Some loggin");

    //        return null;
    //    }
    //} 
    #endregion

    // Request Processors
    // No need to be registered

    public class Pre_PersonQueryHandler : IRequestPreProcessor<PersonQuery>
    {
        public Task Process(PersonQuery request, CancellationToken cancellationToken)
        {
            request.Name = "Name changed in pre processor";
            return Task.CompletedTask;
        }
    }

    public class Post_PersonQueryHandler : IRequestPostProcessor<PersonQuery, PersonResponse>
    {
        public Task Process(PersonQuery request, PersonResponse response, CancellationToken cancellationToken)
        {
            bool hmm = request.Name != null;

            return Task.CompletedTask;
        }
    }
}
