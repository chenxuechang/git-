using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Application
{
    /// <summary>
    /// 真正的写法之地
    /// </summary>
    public static class EntityToEntity
    {
        private static IList<PropertyMapper> GetMapperProperties(Type sourceType, Type targetType)//把值返回到PropertyMapper
        {
            var sourceProperties = sourceType.GetProperties();

            var targetProperties = targetType.GetProperties();
            return (from s in sourceProperties

                    from t in targetProperties

                    where s.Name == t.Name && s.CanRead && t.CanWrite && s.PropertyType == t.PropertyType//需要在定义地定义实体
                    select new PropertyMapper
                    {
                        SourceProperty = s,

                        TargetProperty = t

                    }).ToList();
        }

        public static void CopyProperties(object source, object target, bool bCopyNull = true, List<string> lsExcludeProeprtys = null)
        {
            var sourceType = source.GetType();//获取当前实例
            var targetType = target.GetType();
            var mapperProperties = GetMapperProperties(sourceType, targetType);
            for (int index = 0, count = mapperProperties.Count; index < count; index++)

            {

                var property = mapperProperties[index];//输出的实体类

                var sourceValue = property.SourceProperty.GetValue(source, null);

                if (lsExcludeProeprtys != null && lsExcludeProeprtys.Count > 0)

                {
                    if (lsExcludeProeprtys.Contains(((PropertyInfo)property.SourceProperty).Name)) continue;

                }
                if (!bCopyNull)

                {

                    Type type = ((PropertyInfo)property.SourceProperty).PropertyType;

                    if (sourceValue == null) continue;

                    else if (type == typeof(DateTime) && (DateTime)sourceValue == DateTime.MinValue) continue;

                    else if (type == typeof(int) && (int)sourceValue == 0) continue;

                    else if (type == typeof(decimal) && (decimal)sourceValue == 0) continue;

                }
                property.TargetProperty.SetValue(target, sourceValue, null);
            }
        }
        /// <summary>
        /// 公共静态，为对象数据提供程序
        /// </summary>
        /// <param name="target"></param>
        /// <param name="dataProvider"></param>
        /// <param name="bCopyNull"></param>
        /// <param name="lsExcludeProeprtys"></param>
        //公共静态，为对象数据提供程序
        public static void SetPropertieValues(this object target, object dataProvider, bool bCopyNull = true, List<string> lsExcludeProeprtys = null)
        {
            CopyProperties(dataProvider, target, bCopyNull, lsExcludeProeprtys);
        }
    }
}
