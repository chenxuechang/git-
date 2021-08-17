using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Cathy
{
    public static class ConfigHelper
    {
        public static IConfigurationSection GetSection(string key)
        {
            string strRootPath = Assembly.GetEntryAssembly().Location;
            strRootPath = strRootPath.Substring(0, strRootPath.LastIndexOf("\\"));
            var builder = new ConfigurationBuilder().SetBasePath(strRootPath).AddJsonFile("appsettings.json");
             var configurationRoot = builder.Build();
            return configurationRoot.GetSection(key);
        }
    }
}
