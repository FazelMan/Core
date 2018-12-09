using FazelMan.Core.Common.Context;
using CoreApp.Api.Helper;
using Microsoft.Extensions.Configuration;

namespace FazelMan.Core.Common.CustomConfiguration
{
    public class CustomConfiguration : ICustomConfiguration
    {
        private static IConfiguration _configuration;
        private static IContext _context;

        public CustomConfiguration(IConfiguration configuration, IContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public bool IsLocal()
        {
            var host = _context.Uri().Host;
            return host == "webapp.coreApp.com" || host == _configuration["LocalUrl"] || host == "localhost";
        }

        public bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }

}
