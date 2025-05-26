using FluentValidation;
using Inventarios_Controller.Request;

namespace Inventarios_Controller.Validators
{
    public class ProductValidator: AbstractValidator<ProductRequest>
    {
        public ProductValidator() { 
            RuleFor(x=> x.ProductPrice).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x=> x.ProductCount).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x=> x.ProductKey).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.ProductName).NotEmpty().NotNull();
        }
    }

    public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.ProductPrice).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.ProductKey).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.ProductName).NotEmpty().NotNull();
        }
    }
}
