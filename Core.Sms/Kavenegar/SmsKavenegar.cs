using System;
using System.Threading.Tasks;
using Kavenegar;
using Microsoft.Extensions.Configuration;

namespace Core.Sms.Kavenegar
{
    public class SmsKavenegar
    {
        private readonly IConfiguration _configuration;

        public static string ApiKey { get; set; }
        public string PhoneNumber { get; set; }
        public static string URL { get; set; }

        public SmsKavenegar(IConfiguration configuration)
        {
            _configuration = configuration;
            URL = _configuration["Sms:Kavenegar:URL"];
            PhoneNumber = _configuration["Sms:Kavenegar:PhoneNumber"];
            ApiKey = _configuration["Sms:Kavenegar:ApiKey"];
        }

        public async Task<bool> SendAsync(string token, string phoneNumber, string templateName)
        {
            try
            {
                var api = new KavenegarApi(ApiKey);
                var smsTemplateName = _configuration["Sms:Kavenegar:Templates:" + templateName];
                await api.VerifyLookup(phoneNumber, token.Trim(), smsTemplateName);
                return true;
            }
            catch (global::Kavenegar.Core.Exceptions.ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
                return false;
            }
            catch (global::Kavenegar.Core.Exceptions.HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.Write("Message : " + ex.Message);
                return false;
            }
        }
        public async Task<bool> SendAsync(string token, string token2, string phoneNumber, string templateName)
        {
            try
            {
                var api = new KavenegarApi(ApiKey);
                var smsTemplateName = _configuration["Sms:Kavenegar:Templates:" + templateName];
                await api.VerifyLookup(phoneNumber, token.Trim(), token2.Trim(), "", smsTemplateName);
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
        public async Task<bool> SendAsync(string token, string token2, string token3, string phoneNumber, string templateName)
        {
            try
            {
                var api = new KavenegarApi(ApiKey);
                var smsTemplateName = _configuration["Sms:Kavenegar:Templates:" + templateName];
                await api.VerifyLookup(phoneNumber, token.Trim(), token2.Trim(), token3.Trim(), smsTemplateName);
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
