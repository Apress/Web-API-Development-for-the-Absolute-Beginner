using AutoMapper;
using Conference.Domain.Entities;
using ConferenceApi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ConferenceApi.Infrastructure.ModelBinders
{
    /// <summary>
    /// public IActionResult GetById([ModelBinder(Name = "id")] Speaker speaker)
    /// The ModelBinder attribute can be used to apply the SpeakerEntityBinder to parameters that don't use default conventions:
    /// </summary>
    /// 

    public class ExampleBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            throw new NotImplementedException();
        }
    }

    public class SpeakerEntityBinder : IModelBinder
    {
        private readonly ConferenceContext _context;
        private readonly IMapper mapper;

        public SpeakerEntityBinder(ConferenceContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;

            // Try to fetch the value of the argument by name
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            // Check if the argument value is null or empty
            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            if (!int.TryParse(value, out var id))
            {
                // Non-integer arguments result in model state errors
                bindingContext.ModelState.TryAddModelError(
                    modelName, "Speaker Id must be an integer.");

                return Task.CompletedTask;
            }

            // Model will be null if not found, including for
            // out of range id values (0, -3, etc.)
            var model = _context.Speakers.Find(id);

            var finalModel = mapper.Map<SpeakerModel>(model);
            bindingContext.Result = ModelBindingResult.Success(finalModel);
            return Task.CompletedTask;
        }
    }
}
