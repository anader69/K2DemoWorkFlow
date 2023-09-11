using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Internal;
using FluentValidation.Resources;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Framework.Web.Validators
{
    public class RequiredIfValidator : PropertyValidator
    {
        public string DependentProperty { get; set; }
        public object TargetValue { get; set; }

        public RequiredIfValidator(string dependentProperty, object targetValue) : base(new LanguageStringSource(nameof(RequiredIfValidator)))
        {
            DependentProperty = dependentProperty;
            TargetValue = targetValue;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            //This is not a server side validation rule. So, should not effect at the server side
            return true;
        }
    }

    public class RequiredIfClientValidator : ClientValidatorBase
    {
        RequiredIfValidator RequiredIfValidator => (RequiredIfValidator)Validator;

        public RequiredIfClientValidator(PropertyRule rule, IPropertyValidator validator) : base(rule, validator)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-requiredif", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-requiredif-dependentproperty", RequiredIfValidator.DependentProperty);
            MergeAttribute(context.Attributes, "data-val-requiredif-targetvalue", RequiredIfValidator.TargetValue.ToString());
        }

        private string GetErrorMessage(ClientModelValidationContext context)
        {
            var formatter = ValidatorOptions.MessageFormatterFactory().AppendPropertyName(Rule.GetDisplayName());
            string messageTemplate;
            try
            {
                messageTemplate = Validator.Options.ErrorMessageSource.GetString(null);
            }
            catch (FluentValidationMessageFormatException)
            {
                messageTemplate = ValidatorOptions.LanguageManager.GetStringForValidator<NotEmptyValidator>();
            }
            var message = formatter.BuildMessage(messageTemplate);
            return message;
        }
    }

}
