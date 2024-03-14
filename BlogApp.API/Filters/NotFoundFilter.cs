using BlogApp.Core.Models;
using BlogApp.Core.Services;
using BlogApp.Service.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlogApp.API.Filters
{
    public class NotFoundFilter<T,TDto> : IAsyncActionFilter where T : BaseEntity where TDto : class
    {
        private readonly IService<T, TDto> _service;

        public NotFoundFilter(IService<T, TDto> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault("id");
            if (idValue == null)
            {
                await next.Invoke();
                return;
            }

            var id = (int)idValue;
            var anyEntity = await _service.AnyAsync(x => x.Id == id);

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            throw new NotFoundException(message: $"{typeof(T).Name} is not found");
        }
    }
}
