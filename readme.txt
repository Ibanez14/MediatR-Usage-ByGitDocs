Testing MediatR 
By Github Docs

// Should be done
=> services.AddMediatR


- IRequest<T>  // req that return T value
- IRequest		// req that doesn't return a value


IRequestHandler<RT,RS>		=> Handler RT request and return RS response, good for Query requests
AsyncRequestHandler<T>		=> Handler, take T request return nothing, good for Command requests
RequestHandler<T>			=> Same as above, but sync


INotification				=> The thing that should be notified about
INotificationHandler<T>		=> Handler, take T INotification and return nothing. Can create as many handler as you like  and will be called one after another


IPipeLineBehavior<Rq,Rs>	=> Called before and after Handler. Can be create as many as you want. ! Should be registered


IRequestPreProcessor		=> Called before handler
IRequestPostProcessor		=> Called after handler