using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Cathy
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
            SqlParameter[] sqlPara ={
                                   new SqlParameter("@tableName",typeof(T).Name),
                                   new SqlParameter("@keyValue",SqlDbType.Decimal,18,ParameterDirection.ReturnValue,true,0,0,"",DataRowVersion.Default,DBNull.Value)
                                   };

            SqlParameter[] returnValue = Cathy.SqlHelper.ExecProcedure("Default", sqlPara, "P_GENERATE_ID");
            return returnValue[sqlPara.Length - 1].Value + "";
        }

        /// <summary>
        /// 创建新ID
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="conn">连接字符串KEY值</param>
        /// <returns>新ID</returns>
        public static string CreateId<T>(this T entity, string conn) where T : new()
        {
            SqlParameter[] sqlPara ={
                                   new SqlParameter("@tableName",typeof(T).Name),
                                   new SqlParameter("@keyValue",SqlDbType.Decimal,18,ParameterDirection.ReturnValue,true,0,0,"",DataRowVersion.Default,DBNull.Value)
                                   };

            SqlParameter[] returnValue = Cathy.SqlHelper.ExecProcedure(conn, sqlPara, "P_GENERATE_ID");
            return returnValue[sqlPara.Length - 1].Value + "";
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns>影响行数</returns>
        public static int Insert<T>(this T entity) where T : new()
        {
            SqlEntityOper seo = new SqlEntityOper();
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
        public static int Insert<T>(this T entity, SqlConnection conn, SqlTransaction sqlTran) where T : new()
        {
            SqlEntityOper seo = new SqlEntityOper();
            return seo.ExecuteNonquery(entity, DbType.Insert, sqlTran, true, conn);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="strWhere">更新条件 f0={@f0} and f1={@f1}</param>
        /// <param name="para">条件值</param>
        /// <returns>影响行数</returns>
        public static int Update<T>(this T entity, string strWhere, params object[] para)
        {
            List<SqlParameter> paraList = new List<SqlParameter>();
            #region where条件参数
            MatchCollection mc = Regex.Matches(strWhere, @"\{.+?\}");
            for (int i = 0; i < mc.Count; i++)
            {
                string paraName = mc[i].Value.Replace("{", "").Replace("}", "");
                paraList.Add(new SqlParameter(paraName, para[i]));
            }
            #endregion
            SqlEntityOper seo = new SqlEntityOper();
            string strConn = "";
            string strSql = seo.JoneSqlString(entity, DbType.Update, strWhere, ref strConn, ref paraList);
            return SqlHelper.ExecuteNonQuery(strSql, strConn, paraList);
        }

        /// <summary>
        /// 更新（执行事务）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="strWhere">更新条件 f0={@f0} and f1={@f1}</param>
        /// <param name="conn"> sqlconnection连接 </param>
        /// <param name="sqlTran">sqltransaction</param>
        /// <param name="para">条件值</param>
        /// <returns>影响行数</returns>
        public static int Update<T>(this T entity, string strWhere, SqlConnection conn, SqlTransaction sqlTran, params object[] para)
        {
            List<SqlParameter> paraList = new List<SqlParameter>();
            #region where条件参数
            MatchCollection mc = Regex.Matches(strWhere, @"\{.+?\}");
            for (int i = 0; i < mc.Count; i++)
            {
                string paraName = mc[i].Value.Replace("{", "").Replace("}", "");
                paraList.Add(new SqlParameter(paraName, para[i]));
            }
            #endregion


            SqlEntityOper seo = new SqlEntityOper();
            string strConn = "";
            string strSql = seo.JoneSqlString(entity, DbType.Update, strWhere, ref strConn, ref paraList);
            return SqlHelper.ExecuteNonQuery(strSql, conn, sqlTran, paraList);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="strWhere">删除条件 f0={@f0} and f1={@f1}</param>
        /// <param name="para">条件值</param>
        /// <returns>影响行数</returns>
        public static int Delete<T>(this T entity, string strWhere, params object[] para)
        {
            List<SqlParameter> paraList = new List<SqlParameter>();
            #region where条件参数
            MatchCollection mc = Regex.Matches(strWhere, @"\{.+?\}");
            for (int i = 0; i < mc.Count; i++)
            {
                string paraName = mc[i].Value.Replace("{", "").Replace("}", "");
                paraList.Add(new SqlParameter(paraName, para[i]));
            }
            #endregion
            SqlEntityOper seo = new SqlEntityOper();
            string strConn = "";
            string strSql = seo.JoneSqlString(entity, DbType.Delete, strWhere, ref strConn, ref paraList);
            return SqlHelper.ExecuteNonQuery(strSql, strConn, paraList);
        }

        /// <summary>
        /// 删除（执行事务）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="strWhere">删除条件 f0={@f0} and f1={@f1}</param>
        /// <param name="conn"> sqlconnection连接 </param>
        /// <param name="sqlTran">sqltransaction</param>
        /// <param name="para">条件值</param>
        /// <returns>影响行数</returns>
        public static int Delete<T>(this T entity, string strWhere, SqlConnection conn, SqlTransaction sqlTran, params object[] para)
        {
            List<SqlParameter> paraList = new List<SqlParameter>();
            #region where条件参数
            MatchCollection mc = Regex.Matches(strWhere, @"\{.+?\}");
            for (int i = 0; i < mc.Count; i++)
            {
                string paraName = mc[i].Value.Replace("{", "").Replace("}", "");
                paraList.Add(new SqlParameter(paraName, para[i]));
            }
            #endregion


            SqlEntityOper seo = new SqlEntityOper();
            string strConn = "";
            string strSql = seo.JoneSqlString(entity, DbType.Delete, strWhere, ref strConn, ref paraList);
            return SqlHelper.ExecuteNonQuery(strSql, conn, sqlTran, paraList);
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
            List<SqlParameter> paraList = new List<SqlParameter>();//应该是where条件参数，第一次见，感觉像仓储
            #region where条件参数
            MatchCollection mc = Regex.Matches(strWhere, @"\{.+?\}");//通过迭代正则表达式找到符合格式的数据
            for (int i = 0; i < mc.Count; i++)//      //Count获取匹配的数目，返回匹配的数目
            {
                string paraName = mc[i].Value.Replace("{", "").Replace("}", "");//Replace返回一个新字符串，其中指定字符串在当前实例将替换为另一个指定的字符串，等效的。如果没有找到实列则返回未更改的当前实例


                paraList.Add(new SqlParameter(paraName, para[i]));//输出新的数据//查出来的数据        //SqlParameter初始化实列，使用新的参数名和值
            }
            #endregion

            SqlEntityOper seo = new SqlEntityOper();

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
            DataTable dt = SqlHelper.DataSet(strSql, SqlHelper.CreateConn(strConn), paraList);
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
            List<SqlParameter> paraList = new List<SqlParameter>();
            #region where条件参数
            MatchCollection mc = Regex.Matches(strWhere, @"\{.+?\}");
            for (int i = 0; i < mc.Count; i++)
            {
                string paraName = mc[i].Value.Replace("{", "").Replace("}", "");
                paraList.Add(new SqlParameter(paraName, para[i]));
            }
            #endregion

            SqlEntityOper seo = new SqlEntityOper();
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
            DataTable dt = SqlHelper.DataSet(strSql, SqlHelper.CreateConn(strConn), paraList);
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
            List<SqlParameter> paraList = new List<SqlParameter>();
            #region where条件参数
            MatchCollection mc = Regex.Matches(strWhere, @"\{.+?\}");
            for (int i = 0; i < mc.Count; i++)
            {
                string paraName = mc[i].Value.Replace("{", "").Replace("}", "");
                paraList.Add(new SqlParameter(paraName, para[i]));
            }
            #endregion

            SqlEntityOper seo = new SqlEntityOper();
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
            string strSql = string.Format("select ISNULL(max({0}),0) from {1} where {2}", field, t.Name, strWhere0);
            string maxValue = SqlHelper.ExecuteScalar(strSql, SqlHelper.CreateConn(strConn), paraList);
            return maxValue;
        }
        //public static string fanwei<t>(this )
        //public Dto.UserSingleInfoOutput chafanwei(DateTime ks, DateTime js, Dto.InitPassWordInput initPassWordInput)


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
            string strSql = string.Format("select ISNULL(max({0}),0) from {1}", field, t.Name);
            string maxValue = SqlHelper.ExecuteScalar(strSql, SqlHelper.CreateConn(strConn), null);
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
            List<SqlParameter> paraList = new List<SqlParameter>();
            #region where条件参数
            MatchCollection mc = Regex.Matches(strWhere, @"\{.+?\}");
            for (int i = 0; i < mc.Count; i++)
            {
                string paraName = mc[i].Value.Replace("{", "").Replace("}", "");
                paraList.Add(new SqlParameter(paraName, para[i]));
            }
            #endregion

            SqlEntityOper seo = new SqlEntityOper();
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
            DataTable dt = SqlHelper.DataSet(strSql, SqlHelper.CreateConn(strConn), paraList);
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
            List<SqlParameter> paraList = new List<SqlParameter>();
            #region where条件参数
            MatchCollection mc = Regex.Matches(strWhere, @"\{.+?\}");
            for (int i = 0; i < mc.Count; i++)
            {
                string paraName = mc[i].Value.Replace("{", "").Replace("}", "");
                paraList.Add(new SqlParameter(paraName, para[i]));
            }
            #endregion

            SqlEntityOper seo = new SqlEntityOper();
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
            DataTable dt = SqlHelper.DataSet(strSql, SqlHelper.CreateConn(strConn), paraList);
            return DataTableToList<T>(dt);
        }


        public static void DataRowToT<T>(T entity,DataTable dt,DataRow dr)
        {
            Type t = typeof(T);
            PropertyInfo[] propers = t.GetProperties();
            var plist = new List<PropertyInfo>(propers);
            //T s = System.Activator.CreateInstance<T>();
            for (int i = 0; i <dt.Columns.Count; i++)
            {
                string strClm = dt.Columns[i].ColumnName;
                PropertyInfo info = plist.Find(p => p.Name ==strClm);
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
            PropertyInfo[] props =  type.GetProperties();

            var plist = new List<PropertyInfo>(props);
            foreach (DataRow item in dt.Rows)
            {
                T s = System.Activator.CreateInstance<T>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string strClm = dt.Columns[i].ColumnName;
                    PropertyInfo info = plist.Find(p => p.Name ==strClm);
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
