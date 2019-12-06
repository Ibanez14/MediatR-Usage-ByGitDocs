using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace MediatR2._2WebApplication1.V1_Documentation_Guide.Notifications
{

    //
    // NOTIFICATION
    //

    /// <summary>
    /// Sound notification that is handler by two handlers specified below
    /// </summary>
    public class SoundNotification : INotification
    {
        
        public SoundNotification(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }


    // --------
    // HANDLERS
    // --------

    /// <summary>
    /// First notification handler
    /// </summary>
    public class SoundNotificationHandlerConsole : INotificationHandler<SoundNotification>
    {
        private readonly ILogger<SoundNotificationHandlerConsole> logger;
        public SoundNotificationHandlerConsole(ILogger<SoundNotificationHandlerConsole> logger) =>
            this.logger = logger;



        public Task Handle(SoundNotification notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Notification {notification.Message}");
            // some logic            
            return Task.CompletedTask;
        }
    }
    

    /// <summary>
    /// Second notification handler
    /// </summary>
    public class SoundNotificationHandlerDebug : INotificationHandler<SoundNotification>
    {
        public Task Handle(SoundNotification notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine(notification.Message);
            return Task.CompletedTask;
        }
    }

}
