using Application.Common.Interfaces;
using Application.Users.Commands.Register;
using Application.Users.Queries.Login;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviour
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;

        public RequestLogger(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.GetUserId() ?? string.Empty;
            var userName = _currentUserService.GetUsername() ?? string.Empty;
            var twq = _currentUserService.GetUserGlobalization();

            if (requestName == nameof(LoginQuery) || requestName == nameof(RegisterCommand))
            {
                _logger.LogInformation("Request: {Name} {@UserId} {@UserName}",
                        requestName, userId, userName);
            }
            else
            {
                _logger.LogInformation("Request: {Name} {@UserId} {@UserName} {@Request}",
                        requestName, userId, userName, request);
            }


            return Task.CompletedTask;
        }
    }

}
