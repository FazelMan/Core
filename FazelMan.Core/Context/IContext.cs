using System;
using Microsoft.AspNetCore.Http;

namespace FazelMan.Core.Context
{
    public interface IContext
    {
        Uri Uri();
        string GetHostDomain();
        HttpRequest GetHttpRequest();
        Guid GetUserId();
    }
}
