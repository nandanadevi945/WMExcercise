using FluentValidation;
using System.Text.RegularExpressions;

namespace WM.Api.Models.Validators
{
    public class NewGlobalUniqueIdentifierModelValidator : AbstractValidator<NewGlobalUniqueIdentifierModel>
    {
        public NewGlobalUniqueIdentifierModelValidator()
        {
            RuleFor(x => x.Usr).NotEmpty().WithMessage("User must not be empty");
            RuleFor(x => x.Guid).Must((y, guid) => ValidateGuid(y.Guid)).WithMessage("Must be 32 char ");
        }

        private bool ValidateGuid(String input)
        {
            Regex regex = new Regex("^[A-Z0-9]{32}$");
            return !string.IsNullOrEmpty(input) && regex.IsMatch(input);
        }
    }
}
