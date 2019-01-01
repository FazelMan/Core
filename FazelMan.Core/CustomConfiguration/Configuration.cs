using FazelMan.Core.Context;
using Microsoft.Extensions.Configuration;

namespace FazelMan.Core.CustomConfiguration
{
    public class CustomConfiguration : ICustomConfiguration
    {
        private static IConfiguration _configuration;
        private static IContext _context;
        public string HostUri { get; set; }
        public CustomConfiguration(IConfiguration configuration, IContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public bool IsLocal()
        {
            var host = _context.Uri().Host;
            return host ==  _configuration["Host:LocalUrl"] || host == "localhost";
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
