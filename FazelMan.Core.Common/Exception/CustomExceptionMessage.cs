using System;
using Core.Common.Enums;
using Microsoft.Extensions.Configuration;

namespace FazelMan.Core.Common.Exception
{
    public static class CustomExceptionMessage
    {
        public static string GetExceptionCode(this IConfiguration configuration, ErrorMessage errorCode)
        {
            var _prefixException = configuration["App:CustomExceptionPrefix"];
            return _prefixException + Enum.GetName(typeof(ErrorMessage), errorCode);
        }
    }
}
