using FluentValidation;
using Inventarios_Controller.Request;

namespace Inventarios_Controller.Validators
{
    public class OrdersValidator : AbstractValidator<OrderRequest>
    {
        public OrdersValidator()
        {
            RuleFor(x => x.CustomerName).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0);
            RuleFor(x => x.CustomerName).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty().GreaterThan(0);
        }
    }
}
