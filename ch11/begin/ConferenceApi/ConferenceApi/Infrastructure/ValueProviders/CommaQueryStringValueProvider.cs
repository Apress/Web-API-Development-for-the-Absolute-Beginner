using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using System;
using System.Globalization;
using System.Linq;

namespace Conference.Api.Infrastructure.ValueProviders
{

    public class CommaQueryStringValueProvider : QueryStringValueProvider
    {
        private readonly string separator = ",";

        public CommaQueryStringValueProvider(IQueryCollection values)
            : base(BindingSource.Query, values, CultureInfo.InvariantCulture)
        {
        }

        public override ValueProviderResult GetValue(string key)
        {
            var result = base.GetValue(key);
            if (result != ValueProviderResult.None && result.Values.Any(x => x.IndexOf(separator, StringComparison.OrdinalIgnoreCase) > -1))
            {
                var splitValues = new StringValues(result.Values.SelectMany(x => x.Trim().Split(new[] { separator }, StringSplitOptions.None)).ToArray());
                return new ValueProviderResult(splitValues, result.Culture);
            }

            return result;
        }
    }
}
