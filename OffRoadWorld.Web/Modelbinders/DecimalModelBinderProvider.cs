using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OffRoadWorld.Web.Modelbinders
{
    public class DecimalModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if(context.Metadata.ModelType == typeof(Decimal) || context.Metadata.ModelType == typeof(Decimal?))
            {
                return new DecimalModelBinder(context.Metadata);
            }

            return null!;
        }
    }
}