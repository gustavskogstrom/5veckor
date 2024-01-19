using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Middleware
{
    public class Register
    {
        private readonly RequestDelegate _next;

        public Register(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var blockedEndpoints = new List<string> { "/register", "/refresh", "/confirmEmail", "/resendConfirmEmail", "/forgotPassword", "/resetPassword", "/manage/2fa", "/manage/info", "/manage/info" };

            if (blockedEndpoints.Contains(context.Request.Path.Value.ToLower()))
            {

                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }


            await _next(context);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseMiddleware<Register>();

        }
    }
}