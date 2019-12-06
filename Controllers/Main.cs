using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MediatR___CQRS_Testing.Infrastructure;
using MediatR2._2WebApplication1.V1.CQ_Models;
using MediatR2._2WebApplication1.V1_Documentation_Guide.Notifications;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MediatR___CQRS_Testing.Controllers
{
    [Route("api/[controller]/[action]")]
    public class MainController : Controller
    {
        // Context is used only for seeding
        readonly DataContext context;
        readonly IMediator mediator;

        public MainController(DataContext context, IMediator mediator)
        {
            #region Seeding
            this.context = context;
            this.mediator = mediator;
            context.Set<Person>().AddRange(new Person[]
            {
                new Person(){Name = "Steve", Age = 14 },
                new Person(){Name = "Eric", Age = 54 },
                new Person(){Name = "Clapton", Age = 26}
            });
            context.SaveChanges();
            #endregion
        }

        /// <summary>
        /// PersonQuery request is handled by Handler and pre&post processors
        /// see PersonQuery.cs
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> TestPersonQuery([FromBody] PersonQuery request)
        {
            // See comments
            PersonResponse response = await mediator.Send(request);
            return Ok(response);
        }


        /// <summary>
        /// PersonCommand is handler by Handler and PipeLines
        /// see PersonCommand.cs
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> TestPersonCommand([FromBody] PersonCommand request)
        {
            // See comments
            bool IsAdded = await mediator.Send(request);
            return Ok($"Success on Add {IsAdded}");
        }


        /// <summary>
        /// See Notification.cs
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IActionResult TestSoundNotiication()
        {
            // See comment

            var sound = new SoundNotification("Clacson");
            // maybe sending something on mediator
            // then some publishing
            mediator.Publish(sound);
            return Ok();
        }


    }

}
