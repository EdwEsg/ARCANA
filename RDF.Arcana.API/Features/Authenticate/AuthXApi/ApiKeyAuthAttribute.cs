using Microsoft.AspNetCore.Mvc.Filters;

namespace RDF.Arcana.API.Features.Authenticate.AuthXApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }
    }
}
