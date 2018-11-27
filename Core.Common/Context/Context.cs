using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Core.Common.Context
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
            var request = GetHttpRequest();
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.Host;
            uriBuilder.Path = request.Path.ToString();
            uriBuilder.Query = request.QueryString.ToString();
            return uriBuilder.Uri;
        }

        public string GetHostDomain()
        {
            return $"{GetHttpRequest().Scheme}://{GetHttpRequest().Host}";
        }

        public HttpRequest GetHttpRequest()
        {
            return _httpContextAccessor.HttpContext.Request;
        }

        public Guid? GetUserId()
        {
            if (_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return null;
            }
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(userId);
        }
    }

}
