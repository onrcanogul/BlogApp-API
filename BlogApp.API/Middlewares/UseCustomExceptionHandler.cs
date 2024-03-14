using BlogApp.Core.Dtos;
using BlogApp.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BlogApp.API.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exceptionFeature.Error switch
                    {
                        CustomException => 400,
                        NotFoundException => 404,
                        _ => 500
                    };

                    context.Response.StatusCode = statusCode;
                    var response =
                        CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message, true);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });


            });
            }
        }
    }
