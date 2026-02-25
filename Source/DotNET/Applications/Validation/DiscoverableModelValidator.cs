// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cratis.Applications.Validation;

/// <summary>
/// Represents a <see cref="IModelValidator"/> for <see cref="BaseValidator{T}"/>.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DiscoverableModelValidator"/> class.
/// </remarks>
/// <param name="validator">The <see cref="IValidator"/> to use.</param>
public class DiscoverableModelValidator(IValidator validator) : IModelValidator
{
    /// <summary>
    /// The key used to store <see cref="ValidationFailure"/> instances in <see cref="Microsoft.AspNetCore.Http.HttpContext.Items"/>.
    /// </summary>
    public const string ValidationFailuresKey = "Cratis.ValidationFailures";

    /// <inheritdoc/>
    public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
    {
        if (context.Model is not null)
        {
            var validationContextType = typeof(ValidationContext<>).MakeGenericType(context.ModelMetadata.ModelType);
            var validationContext = (Activator.CreateInstance(validationContextType, [context.Model!]) as IValidationContext)!;

            SetValidationType(context, validationContext);

            var result = validator.ValidateAsync(validationContext).GetAwaiter().GetResult();
            if (result.Errors.Count > 0)
            {
                var httpContext = context.ActionContext.HttpContext;
                var existing = httpContext.Items[ValidationFailuresKey] as List<ValidationFailure> ?? [];
                existing.AddRange(result.Errors);
                httpContext.Items[ValidationFailuresKey] = existing;
            }
        }

        // Validation failures are stored in HttpContext.Items; do not pollute ModelState.
        return [];
    }

    void SetValidationType(ModelValidationContext context, IValidationContext validationContext)
    {
        if (context.ActionContext.HttpContext.Request.Method == HttpMethod.Post.Method)
        {
            validationContext.SetCommand();
        }
        else
        {
            validationContext.SetQuery();
        }
    }
}
