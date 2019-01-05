using System;
using System.Threading.Tasks;
using Kavenegar;
using Microsoft.Extensions.Configuration;

namespace FazelMan.Sms.Kavenegar.Kavenegar
{
    public class SmsKavenegar : ISmsService
    {
        private readonly IConfiguration _configuration;

        private static string ApiKey { get; set; }
        private string PhoneNumber { get; set; }
        private static string Url { get; set; }

        public SmsKavenegar(IConfiguration configuration)
        {
            _configuration = configuration;
            Url = _configuration["FazelMan:Sms.Kavenegar:URL"];
            PhoneNumber = _configuration["FazelMan:Sms.Kavenegar:PhoneNumber"];
            ApiKey = _configuration["FazelMan:Sms.Kavenegar:ApiKey"];
        }

        public async Task<bool> SendAsync(string templateName, string phoneNumber, string value)
        {
            try
            {
                var api = new KavenegarApi(ApiKey);
                var smsTemplateName = _configuration["FazelMan:Sms.Kavenegar:Templates:" + templateName];
                await api.VerifyLookup(phoneNumber, value.Trim(), smsTemplateName);
                return true;
            }
            catch (global::Kavenegar.Core.Exceptions.ApiException ex)
            {
                //if it doesn't return 200 
                Console.Write("Message : " + ex.Message);
                return false;
            }
            catch (global::Kavenegar.Core.Exceptions.HttpException ex)
            {
                //if connect to service error occured 
                Console.Write("Message : " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.Write("Message : " + ex.Message);
                return false;
            }
        }

        public async Task<bool> SendAsync(string templateName, string phoneNumber, params string[] values)
        {
            try
            {
                var api = new KavenegarApi(ApiKey);
                var smsTemplateName = _configuration["FazelMan:Sms.Kavenegar:Templates:" + templateName];
                
                await api.VerifyLookup(phoneNumber, values[0].Trim(), values[1].Trim(), values[2].Trim(), values[3].Trim(), smsTemplateName);
                return true;
            }
            catch (global::Kavenegar.Core.Exceptions.ApiException ex)
            {
                Console.Write("Message : " + ex.Message);
                return false;
            }
            catch (global::Kavenegar.Core.Exceptions.HttpException ex)
            {
                Console.Write("Message : " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.Write("Message : " + ex.Message);
                return false;
            }
        }
    }

}
