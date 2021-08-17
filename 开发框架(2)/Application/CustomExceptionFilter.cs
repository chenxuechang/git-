using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application
{
    public class CustomExceptionFilter : Attribute, IExceptionFilter
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public CustomExceptionFilter(IHostingEnvironment hostingEnvironment,
          IModelMetadataProvider modelMetadataProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }
        /// <summary>
        /// 发生异常进入
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)//如果异常没有处理
            {
                Controllers.Dto.ResultInfo ri = new Controllers.Dto.ResultInfo();
                ri.IsSuccess = 0;
                ri.ErrorInfo = context.Exception.Message;
                object objResult = new { resultInfo = ri };
                context.Result = new JsonResult(objResult);
                Log.LogHelper.Error(context.ActionDescriptor.AttributeRouteInfo.Template + ":" + context.Exception.Message);
                //异常已处理
                context.ExceptionHandled = true;
            }
        }
    }
}
