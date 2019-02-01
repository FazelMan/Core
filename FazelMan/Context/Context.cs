using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace FazelMan.Context
{
    public class Context : IContext
    {
        private static IHttpContextAccessor _httpContextAccessor;
        public Context(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Uri Uri()
        {
            var httpContext = GetHttpContext();
            var uriBuilder = new UriBuilder
            {
                Scheme = httpContext.Request.Scheme,
                Host = httpContext.Request.Host.Host,
                Path = httpContext.Request.Path.ToString(),
                Query = httpContext.Request.QueryString.ToString()
            };
            return uriBuilder.Uri;
        }

        public string GetHostDomain()
        {
            return $"{GetHttpContext().Request.Scheme}://{GetHttpContext().Request.Host}";
        }

        public HttpContext GetHttpContext()
        {
            return _httpContextAccessor.HttpContext;
        }
        

        public Guid GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(userId);
        }
    }

}
