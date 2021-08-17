using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Cathy.MySql
{
    public static class DbOperExtend
    {
        /// <summary>
        /// 创建新ID
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns>新ID</returns>
        public static string CreateId<T>(this T entity) where T : new()
        {
            string strSql = "select F_NEXTVAL('"+typeof(T).Name+"')";
            return MySqlHelper.ExecuteScalar(strSql, MySqlHelper.CreateConn());
        }

        /// <summary>
        /// 创建新ID
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="conn">连接字符串KEY值</param>
        /// <returns>新ID</returns>
        public static string CreateId<T>(this T entity, string conn) where T : new()
        {
            string strSql = "select F_NEXTVAL('" + typeof(T).Name + "')";
            return MySqlHelper.ExecuteScalar(strSql, MySqlHelper.CreateConn(conn));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns>影响行数</returns>
        public static int Insert<T>(this T entity) where T : new()
        {
            MySqlEntityOper seo = new MySqlEntityOper();
            return seo.ExecuteNonquery(entity, DbType.Insert);
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="sqlTran">sql事务</param>
        /// <returns>影响行数</returns>
        public static int Insert<T>(this T entity, MySqlConnection conn, MySqlTransaction sqlTran) where T : new()
        {
            MySqlEntityOper seo = new MySqlEntityOper();
            return seo.ExecuteNonquery(entity, DbType.Insert, sqlTran, true, conn);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="strWhere">更新条件 f0={?f0} and f1={?f1}</param>
        /// <param name="para">条件值</param>
        /// <returns>影响行数</returns>
        public static int Update<T>(this T entity, string strWhere, params object[] para)
        {
            List<MySqlParameter> paraList = new List<MySqlParameter>();
            #region where条件参数
            MatchCollection mc = Regex.Matches(strWhere, @"\{.+?\}");
            for (int i = 0; i < mc.Count; i++)
            {
                string paraName = mc[i].Value.Replace("{", "").Replace("}", "");
                paraList.Add(new MySqlParameter(paraName, para[i]));
            }
            #endregion
            MySqlEntityOper seo = new MySqlEntityOper();
            string strConn = "";
            string strSql = seo.JoneSqlString(entity, DbType.Update, strWhere, ref strConn, ref paraList);
            MySqlConnection conn = MySqlHelper.CreateConn(strConn);
            return MySqlHelper.ExecuteNonQuery(strSql, conn, paraList);
        }

        /// <summary>
        /// 更新（执行事务）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="strWhere">更新条件 f0={?f0} and f1={?f1}</param>
        /// <param name="conn"> sqlconnection连接 </param>
        /// <param name="sqlTran">sqltransaction</param>
        /// <param name="para">条件值</param>
        /// <returns>影响行数</returns>
        public static int Update<T>(this T entity, string strWhere, MySqlConnection conn, MySqlTransaction sqlTran, params object[] para)
        {
            List<MySqlParameter> paraList = new List<MySqlParameter>();
            #region where条件参数
            MatchCollection mc = Regex.Matches(strWhere, @"\{.+?\}");
            for (int i = 0; i < mc.Count; i++)
            {
                string paraName = mc[i].Value.Replace("{", "").Replace("}", "");
                paraList.Add(new MySqlParameter(paraName, para[i]));
            }
            #endregion


            MySqlEntityOper seo = new MySqlEntityOper();
            string strConn = "";
            string strSql = seo.JoneSqlString(entity, DbType.Update, strWhere, ref strConn, ref paraList);
            return MySqlHelper.ExecuteNonQuery(strSql, conn, sqlTran, paraList);
        }

        /// <summary>
        /// 更新 使用关键字
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="strValue">键值</param>
        /// <returns>影响行数</returns>
        public static int UpdateById<T>(this T entity, object strValue)
        {
            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties();
            string strKey = "";
            foreach (PropertyInfo p in props)
            {
                if (p.GetCustomAttributes(typeof(KeywordAttribute), false).Count() == 1)
                {
                    strKey = p.Name;
                    break;
                }
            }
            return entity.Update(strKey+"={?"+strKey+"}", strValue);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="strWhere">删除条件 f0={?f0} and f1={?f1}</param>
        /// <param name="para">条件值</param>
        /// <returns>影响行数</returns>
        public static int Delete<T>(this T entity, string strWhere, params object[] para)
        {
            List<MySqlParameter> paraList = new List<MySqlParameter>();
            #region where条件参数
            MatchCollection mc = Regex.Matches(strWhere, @"\{.+?\}");
            for (int i = 0; i < mc.Count; i++)
            {
                string paraName = mc[i].Value.Replace("{", "").Replace("}", "");
                paraList.Add(new MySqlParameter(paraName, para[i]));
            }
            #endregion

            MySqlEntityOper seo = new MySqlEntityOper();
            string strConn = "";
            string strSql = seo.JoneSqlString(entity, DbType.Delete, strWhere, ref strConn, ref paraList);
            MySqlConnection conn = MySqlHelper.CreateConn(strConn);
            return MySqlHelper.ExecuteNonQuery(strSql, conn, paraList);
        }

        /// <summary>
        /// 删除（执行事务）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="strWhere">删除条件 f0={?f0} and f1={?f1}</param>
        /// <param name="conn"> sqlconnection连接 </param>
        /// <param name="sqlTran">sqltransaction</param>
        /// <param name="para">条件值</param>
        /// <returns>影响行数</returns>
        public static int Delete<T>(this T entity, string strWhere, MySqlConnection conn, MySqlTransaction sqlTran, params object[] para)
        {
            List<MySqlParameter> paraList = new List<MySqlParameter>();
            #region where条件参数
            MatchCollection mc = Regex.Matches(strWhere, @"\{.+?\}");
            for (int i = 0; i < mc.Count; i++)
            {
                string paraName = mc[i].Value.Replace("{", "").Replace("}", "");
                paraList.Add(new MySqlParameter(paraName, para[i]));
            }
            #endregion


            MySqlEntityOper seo = new MySqlEntityOper();
            string strConn = "";
            string strSql = seo.JoneSqlString(entity, DbType.Delete, strWhere, ref strConn, ref paraList);
            return MySqlHelper.ExecuteNonQuery(strSql, conn, sqlTran, paraList);
        }

        /// <summary>
        /// 删除  使用关键字
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="strValue">键值</param>
        /// <returns>影响行数</returns>
        public static int DeleteById<T>(this T entity, object strValue)
        {
            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties();
            string strKey = "";
            foreach (PropertyInfo p in props)
            {
                if (p.GetCustomAttributes(typeof(KeywordAttribute), false).Count() == 1)
                {
                    strKey = p.Name;
                    break;
                }
            }
            return entity.Delete(strKey + "={?" + strKey + "}", strValue);
        }

        /// <summary>
        /// 选择一条记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="fields">查询的字段</param>
        /// <param name="strWhere">where条件</param>
        /// <param name="para">sqlparametor参数值</param>
        /// <returns>是否存在记录 存在:true 不存在:false 并为实体赋值</returns>
        public static bool Select<T>(this T entity, string fields, string strWhere, params object[] para)
        {
            List<MySqlParameter> paraList = new List<MySqlParameter>();
            #region where条件参数
            MatchCollection mc = Regex.Matches(strWhere, @"\{.+?\}");
            for (int i = 0; i < mc.Count; i++)
            {
                string paraName = mc[i].Value.Replace("{", "").Replace("}", "");
                paraList.Add(new MySqlParameter(paraName, para[i]));
            }
            #endregion

            MySqlEntityOper seo = new MySqlEntityOper();
            string strWhere0 = strWhere.Replace("{", "").Replace("}", "");
            Type t = typeof(T);
            PropertyInfo[] pis = t.GetProperties();
            string strConn = "Default";
            foreach (PropertyInfo pi in pis)
            {
                if (pi.Name == "Conn")
                {
                    strConn = pi.GetValue(entity, null) + "";
                }
            }
            string strSql = string.Format("select {0} from {1} where {2}", fields, t.Name, strWhere0);

            DataTable dt = MySqlHelper.DataSet(strSql, MySqlHelper.CreateConn(strConn), paraList);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                DataRowToT<T>(entity, dt, dr);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除  使用关键字
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="strValue">键值</param>
        /// <returns>影响行数</returns>
        public static bool SelectById<T>(this T entity, object strValue)
        {
            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties();
            string strKey = "";
            foreach (PropertyInfo p in props)
            {
                if (p.GetCustomAttributes(typeof(KeywordAttribute), false).Count() == 1)
                {
                    strKey = p.Name;
                    break;
                }
            }
            return entity.Select("*",strKey + "={?" + strKey + "}", strValue);
        }

        /// <summary>
        /// 选择第一条记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="fields">查询的字段</param>
        /// <param name="strWhere">where条件</param>
        /// <param name="order">排序字段</param>
        /// <param name="para">sqlparametor参数值</param>
        /// <returns>是否存在记录 存在:true 不存在:false 并为实体赋值</returns>
        public static bool First<T>(this T entity, string fields, string strWhere, string order, params object[] para)
        {
            List<MySqlParameter> paraList = new List<MySqlParameter>();
            #region where条件参数
            MatchCollection mc = Regex.Matches(strWhere, @"\{.+?\}");
            for (int i = 0; i < mc.Count; i++)
            {
                string paraName = mc[i].Value.Replace("{", "").Replace("}", "");
                paraList.Add(new MySqlParameter(paraName, para[i]));
            }
            #endregion

            MySqlEntityOper seo = new MySqlEntityOper();
            string strWhere0 = strWhere.Replace("{", "").Replace("}", "");
            Type t = typeof(T);
            PropertyInfo[] pis = t.GetProperties();
            string strConn = "Default";
            foreach (PropertyInfo pi in pis)
            {
                if (pi.Name == "Conn")
                {
                    strConn = pi.GetValue(entity, null) + "";
                }
            }
            string strSql = string.Format("select {0} from {1} where {2} order by {3}", fields, t.Name, strWhere0, order);

            DataTable dt = MySqlHelper.DataSet(strSql, MySqlHelper.CreateConn(strConn), paraList);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                DataRowToT<T>(entity, dt, dr);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 取某个字段的最大值
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="field">字段</param>
        /// <param name="strWhere">where条件</param>
        /// <param name="para">where条件参数</param>
        /// <returns>取某个字段的最大值</returns>
        public static string Max<T>(this T entity, string field, string strWhere, params object[] para)
        {
            List<MySqlParameter> paraList = new List<MySqlParameter>();
            #region where条件参数
            MatchCollection mc = Regex.Matches(strWhere, @"\{.+?\}");
            for (int i = 0; i < mc.Count; i++)
            {
                string paraName = mc[i].Value.Replace("{", "").Replace("}", "");
                paraList.Add(new MySqlParameter(paraName, para[i]));
            }
            #endregion

            MySqlEntityOper seo = new MySqlEntityOper();
            string strWhere0 = strWhere.Replace("{", "").Replace("}", "");
            Type t = typeof(T);
            PropertyInfo[] pis = t.GetProperties();
            string strConn = "Default";
            foreach (PropertyInfo pi in pis)
            {
                if (pi.Name == "Conn")
                {
                    strConn = pi.GetValue(entity, null) + "";
                }
            }
            string strSql = string.Format("select IFNULL(max({0}),0) from {1} where {2}", field, t.Name, strWhere0);
            string maxValue = MySqlHelper.ExecuteScalar(strSql, MySqlHelper.CreateConn(strConn), paraList);
            return maxValue;
        }

        /// <summary>
        /// 取某个字段的最大值
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="field">字段</param>
        /// <returns>取某个字段的最大值</returns>
        public static string Max<T>(this T entity, string field)
        {
            Type t = typeof(T);
            PropertyInfo[] pis = t.GetProperties();
            string strConn = "Default";
            foreach (PropertyInfo pi in pis)
            {
                if (pi.Name == "Conn")
                {
                    strConn = pi.GetValue(entity, null) + "";
                }
            }
            string strSql = string.Format("select IFNULL(max({0}),0) from {1}", field, t.Name);
            string maxValue = MySqlHelper.ExecuteScalar(strSql, MySqlHelper.CreateConn(strConn), null);
            return maxValue;
        }

        /// <summary>
        /// 选择记录List
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="fields">查询的字段</param>
        /// <param name="strWhere">where条件</param>
        /// <param name="para">sqlparametor参数值</param>
        /// <returns>选择记录List</returns>
        public static List<T> Fill<T>(this T entity, string fields, string strWhere, params object[] para)
        {
            List<MySqlParameter> paraList = new List<MySqlParameter>();
            #region where条件参数
            MatchCollection mc = Regex.Matches(strWhere, @"\{.+?\}");
            for (int i = 0; i < mc.Count; i++)
            {
                string paraName = mc[i].Value.Replace("{", "").Replace("}", "");
                paraList.Add(new MySqlParameter(paraName, para[i]));
            }
            #endregion

            MySqlEntityOper seo = new MySqlEntityOper();
            string strWhere0 = strWhere.Replace("{", "").Replace("}", "");
            Type t = typeof(T);
            PropertyInfo[] pis = t.GetProperties();
            string strConn = "Default";
            foreach (PropertyInfo pi in pis)
            {
                if (pi.Name == "Conn")
                {
                    strConn = pi.GetValue(entity, null) + "";
                }
            }

            string strSql = string.Format("select {0} from {1} where {2}", fields, t.Name, strWhere0);
            DataTable dt = MySqlHelper.DataSet(strSql, MySqlHelper.CreateConn(strConn), paraList);
            return DataTableToList<T>(dt);
        }

        /// <summary>
        /// 选择记录List
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="fields">查询的字段</param>
        /// <param name="strWhere">where条件</param>
        /// <param name="order">排序字段</param>
        /// <param name="para">sqlparametor参数值</param>
        /// <returns>选择记录List</returns>
        public static List<T> FillOrder<T>(this T entity, string fields, string strWhere, string order, params object[] para)
        {
            List<MySqlParameter> paraList = new List<MySqlParameter>();
            #region where条件参数
            MatchCollection mc = Regex.Matches(strWhere, @"\{.+?\}");
            for (int i = 0; i < mc.Count; i++)
            {
                string paraName = mc[i].Value.Replace("{", "").Replace("}", "");
                paraList.Add(new MySqlParameter(paraName, para[i]));
            }
            #endregion

            MySqlEntityOper seo = new MySqlEntityOper();
            string strWhere0 = strWhere.Replace("{", "").Replace("}", "");
            Type t = typeof(T);
            PropertyInfo[] pis = t.GetProperties();
            string strConn = "Default";
            foreach (PropertyInfo pi in pis)
            {
                if (pi.Name == "Conn")
                {
                    strConn = pi.GetValue(entity, null) + "";
                }
            }
            string strSql = string.Format("select {0} from {1} where {2} order by {3}", fields, t.Name, strWhere0, order);
            DataTable dt = MySqlHelper.DataSet(strSql, MySqlHelper.CreateConn(strConn), paraList);
            return DataTableToList<T>(dt);
        }


        public static void DataRowToT<T>(T entity, DataTable dt, DataRow dr)
        {
            Type t = typeof(T);
            PropertyInfo[] propers = t.GetProperties();
            var plist = new List<PropertyInfo>(propers);
            //T s = System.Activator.CreateInstance<T>();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string strClm = dt.Columns[i].ColumnName.FormatChange();
                PropertyInfo info = plist.Find(p => p.Name == strClm);
                if (info != null)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        info.SetValue(entity, dr[i], null);
                    }
                }
            }
        }

        /// <summary>
        /// 将dataTable转成List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(DataTable dt)
        {
            if (dt == null) return new List<T>();
            var list = new List<T>();
            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties();

            var plist = new List<PropertyInfo>(props);
            foreach (DataRow item in dt.Rows)
            {
                T s = System.Activator.CreateInstance<T>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string strClm = dt.Columns[i].ColumnName.FormatChange();
                    PropertyInfo info = plist.Find(p => p.Name == strClm);
                    if (info != null)
                    {
                        if (!Convert.IsDBNull(item[i]))
                        {
                            info.SetValue(s, item[i], null);
                        }
                    }
                }
                list.Add(s);
            }
            return list;
        }
    }
}
