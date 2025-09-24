using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BackendTraining.Infrastructure.Swagger
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Verifica se o método ou controlador tem [Authorize]
            var hasAuthorize = (context.MethodInfo.DeclaringType?.GetCustomAttributes(true)
                                    .OfType<AuthorizeAttribute>()
                                    .Any() ?? false)
                               || (context.MethodInfo?.GetCustomAttributes(true)
                                    .OfType<AuthorizeAttribute>()
                                    .Any() ?? false);

            if (hasAuthorize)
            {
                operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [ new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            }
                        ] = new string[] { }
                    }
                };
            }
        }
    }
}
