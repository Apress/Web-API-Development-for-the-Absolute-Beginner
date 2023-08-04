using Conference.Api.Infrastructure.ValueProviders;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Conference.Api.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class CommaQueryStringAttribute : Attribute, IResourceFilter
    {
        private readonly CommaQueryStringValueProviderFactory factory;

        public CommaQueryStringAttribute()
        {
            factory = new CommaQueryStringValueProviderFactory();
        }
     
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // will be implemented
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.ValueProviderFactories.Insert(0, factory);
        }
    }
}
