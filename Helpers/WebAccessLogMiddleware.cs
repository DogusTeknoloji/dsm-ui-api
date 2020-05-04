using DSM.UI.Api.Helpers.JwtHelpers;
using DSM.UI.Api.Models.WebAccessLogs;
using DSM.UI.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace DSM.UI.Api.Helpers
{
    public class WebAccessLogMiddleware
    {
        private readonly RequestDelegate _next;
        public WebAccessLogMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            string token = context.Request.Headers["Authorization"];
            DecodedTokenStruct decodedToken;

            if (token != null)
            {
                token = token.Split(' ')[1];
                decodedToken = JwtExtensions.DecodeToken(token);
            }
            else
            {
                decodedToken = new DecodedTokenStruct();
            }

            IWebAccessLogService logService = context.RequestServices.GetService(typeof(IWebAccessLogService))
                                  as IWebAccessLogService;

            TimeSpan? TimeToExpire_TimeSpan;
            DateTime? TimeToExpire = null;
            if (decodedToken.ValidTo != null)
            {
                TimeToExpire_TimeSpan = decodedToken.ValidTo - DateTime.Now;
                TimeToExpire = new DateTime() + TimeToExpire_TimeSpan;
            }

            WebAccessLog accessLog = new WebAccessLog()
            {
                UserIpAddress = context.Connection.RemoteIpAddress.MapToIPv4().ToString(),
                UserPort = context.Connection.RemotePort,
                UserName = decodedToken.Username,
                UserRole = decodedToken.Userrole,
                UserBrowser = context.Request.Headers["User-Agent"],
                CredentialsIssuedAt = decodedToken.IssuedAt,
                CredentialsIssuer = decodedToken.Issuer,
                CredentialsRemainingTimeToExpire = TimeToExpire,
                LogTimeStamp = DateTime.Now,
                RequestMethod = context.Request.Method,
                DestinationIpAddress = context.Connection.LocalIpAddress.MapToIPv4().ToString(),
                DestinationPort = context.Connection.LocalPort,
                ServerResponseCode = context.Response.StatusCode,
                QueryString = context.Request.QueryString.Value,
                IsHttps = context.Request.IsHttps,
                Protocol = context.Request.Protocol,
                RequestUrl = context.Request.Path
            };

            await logService.WriteLogAsync(accessLog);

            await this._next.Invoke(context);
        }
    }

    public static class WALogMiddlewareExtensions
    {
        public static IApplicationBuilder UseWebAccessLog(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WebAccessLogMiddleware>();
        }
    }
}