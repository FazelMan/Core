using Core.Common.Enums;
using Core.Common.Extentions;

namespace Core.Common.UserAgent
{
    public static class UserAgent
    {
        public static ClientTypeEnum GetUserAgentClientType(string userAgent)
        {
            userAgent = userAgent.ToLower();

            if (userAgent.Contains("android"))
                userAgent = "android";

            else if (userAgent.Contains("ipad") || userAgent.Contains("iphone") || userAgent.Contains("ios"))
                userAgent = "ios";

            else
                userAgent = "web";

            return userAgent.GetEnum<ClientTypeEnum>();
        }
    }
}
