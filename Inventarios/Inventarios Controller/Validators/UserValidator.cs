using FluentValidation;
using Inventarios_Controller.Request;

namespace Inventarios_Controller.Validators
{
    public class UserValidator :AbstractValidator<UserRequest>
    {
        public UserValidator() {
            RuleFor(x=> x.Email).NotEmpty().EmailAddress().NotNull();
            RuleFor(x => x.RoleId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x=> x.Password).NotNull().NotEmpty();
            RuleFor(x=> x.Name).NotNull().NotEmpty();
            RuleFor(x=> x.LastName).NotNull().NotEmpty();
        }
    }
    
    public class UpdateUserValidator :AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator() {
            RuleFor(x=> x.Email).NotEmpty().EmailAddress().NotNull();
            RuleFor(x => x.RoleId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x=> x.UserId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x=> x.Name).NotNull().NotEmpty();
            RuleFor(x=> x.LastName).NotNull().NotEmpty();
        }
    }
}
