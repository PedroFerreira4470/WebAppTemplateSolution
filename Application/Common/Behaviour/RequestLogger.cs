using Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviour
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _userService;

        public RequestLogger(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _userService = currentUserService;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {

            if (_userService.UserId is not null)
            {
                var requestName = typeof(TRequest).Name;
                var userId = _userService.UserId;
                var userName = _userService.UserName ?? string.Empty;

                _logger.LogInformation("Request: {Name} {@UserId} {@UserName} {@Request}",
                           requestName, userId, userName, request); 
            }

            return Task.CompletedTask;
        }
    }

}
