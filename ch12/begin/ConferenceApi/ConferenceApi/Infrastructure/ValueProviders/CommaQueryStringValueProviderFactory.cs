using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace Conference.Api.Infrastructure.ValueProviders
{

    public class CommaQueryStringValueProviderFactory : IValueProviderFactory
    {

        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            context.ValueProviders.Insert(0, new CommaQueryStringValueProvider(context.ActionContext.HttpContext.Request.Query));
            return Task.CompletedTask;
        }
    }
}
