using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace OffRoadWorld.Web.Modelbinders
{
    public class DecimalModelBinder : IModelBinder
    {
        private readonly ModelMetadata metadata;

        public DecimalModelBinder(ModelMetadata metadata)
        {
            this.metadata = metadata;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);

            decimal? actualValue = null;
            bool success = false;

            if (valueResult != ValueProviderResult.None && !String.IsNullOrEmpty(valueResult.FirstValue))
            {
                try
                {
                    var decValue = valueResult.FirstValue;
                    
                    decValue = decValue.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    decValue = decValue.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    
                    actualValue = Convert.ToDecimal(decValue, CultureInfo.CurrentCulture);

                    success = true;
                }
                catch (FormatException ex)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex, bindingContext.ModelMetadata);
                }
            }
            else
            {
                if (metadata.ModelType == typeof(Decimal?))
                {
                    success = true;
                }
            }

            if (success)
            {
                bindingContext.Result = ModelBindingResult.Success(actualValue);
                
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}