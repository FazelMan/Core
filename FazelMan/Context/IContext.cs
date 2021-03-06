﻿using System;
using Microsoft.AspNetCore.Http;

namespace FazelMan.Context
{
    public interface IContext
    {
        Uri Uri();
        string GetHostDomain();
        HttpContext GetHttpContext();
        Guid GetUserId();
    }
}
