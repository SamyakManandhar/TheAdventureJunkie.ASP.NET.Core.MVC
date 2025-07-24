using FluentValidation;
using TheAdventureJunkie.Models;
using TheAdventureJunkie.Models.Dto;

namespace TheAdventureJunkie.Validators
{
    public class OrderValidator : AbstractValidator<OrderDto>
    {
        public OrderValidator()
        {
            RuleFor(order => order.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot be more than 50 characters.");
            RuleFor(order => order.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("First name cannot be more than 50 characters.");
            RuleFor(order => order.AddressLine1)
                .NotEmpty().WithMessage("Address line 1 is required.")
                .MaximumLength(100).WithMessage("Address name cannot be more than 100 characters.");
            RuleFor(order => order.ZipCode)
                .NotEmpty().WithMessage("Zip code is required.")
                .Length(4, 10).WithMessage("Zip code must be between 4 and 10 characters.");
            RuleFor(order => order.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City cannot be more than 50 characters.");
            RuleFor(order => order.State)
                .MaximumLength(10).WithMessage("State cannot be more than 10 characters.");
            RuleFor(order => order.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(50).WithMessage("Country cannot be more than 50 characters.");
            RuleFor(order => order.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must be in a valid format.");
            RuleFor(order => order.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.");
        }

    }
}
