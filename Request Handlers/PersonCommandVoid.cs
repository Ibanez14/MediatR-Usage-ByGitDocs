using MediatR;
using MediatR___CQRS_Testing.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR2._2WebApplication1.V1.CQ_Models
{
    //
    // Here we use only Request and Handler
    //


    /// <summary>
    /// Request that doesnt return a response
    /// </summary>
    public class PersonCommandVoid : IRequest
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }





    /// <summary>
    /// Handler that takes command, maps to Person, save in DV, => void
    /// </summary>
    public class PersonCommandVoidHandler : AsyncRequestHandler<PersonCommandVoid>
    {
        private readonly DataContext context;

        public PersonCommandVoidHandler(DataContext context)
        {
            this.context = context;

        }

        protected override async Task Handle(PersonCommandVoid request, CancellationToken cancellationToken)
        {
            context.Set<Person>().Add(new Person()
            {
                Age = request.Age,
                Name = request.Name
            });

            await context.SaveChangesAsync();
        }
    }








}
