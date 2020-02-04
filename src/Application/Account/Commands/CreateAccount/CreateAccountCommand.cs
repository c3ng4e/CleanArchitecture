using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Account.Commands.CreateAccount
{
    class CreateAccountCommand : IRequest<long>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, long>
        {
            private readonly IIdentityService _userManager;

            public CreateAccountCommandHandler(IIdentityService userManager) 
            {
                _userManager = userManager;
            }

            public async Task<long> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
            {
                var result = await _userManager.CreateUserAsync(request.Email, request.Password);
                return 0;
            }
        }
    }
}
