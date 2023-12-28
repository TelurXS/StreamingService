using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using FluentValidation;
using Infrastructure.Configurations;
using MediatR;

namespace Application.Features.Accounts;

public static class CreateAccount
{
    public class Request : IRequest<CreateResult<Account>>
    {
        public required string Name { get; set; }
        
        public required string Login { get; set; }
        
        public required string Email { get; set; }
        
        public required string Password { get; set; }
    }

    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(AccountConfiguration.NAME_MAX_LENGTH);
            
            RuleFor(x => x.Login)
                .NotEmpty()
                .MaximumLength(AccountConfiguration.LOGIN_MAX_LENGTH);
            
            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(AccountConfiguration.EMAIL_MAX_LENGTH);
            
            RuleFor(x => x.Password)
                .NotEmpty()
                .MaximumLength(AccountConfiguration.PASSWORD_MAX_LENGTH);
        }
    }
    
    public class Handler : SyncRequestHandler<Request, CreateResult<Account>>
    {
        public Handler(
            IValidator<Request> validator, 
            IAccountMapper mapper,
            IAccountService accountService)
        {
            Validator = validator;
            AccountService = accountService;
            Mapper = mapper;
        }
        
        private IValidator<Request> Validator { get; }
        private IAccountMapper Mapper { get; }
        private IAccountService AccountService { get; }

        protected override CreateResult<Account> Handle(Request request)
        {
            var validationResult = Validator.Validate(request);

            if (validationResult.IsValid is false)
                return new ValidationFailed(validationResult.Errors);

            var account = Mapper.FromRequest(request);
            
            return AccountService.Create(account);
        }
    }
}