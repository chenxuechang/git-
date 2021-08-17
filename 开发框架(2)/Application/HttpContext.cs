using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class HttpContext
    {
        private static IHttpContextAccessor _contextAccessor;

        /// <summary>
        /// 当前上下文
        /// </summary>
        public static Microsoft.AspNetCore.Http.HttpContext Current => _contextAccessor.HttpContext;


        public static void Configure(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
    }
}
