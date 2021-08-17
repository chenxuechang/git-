using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Cathy
{
    public static class SqlQuery
    {
        /// <summary>
        /// 返回List[T]
        /// </summary>
        /// <param name="entity">查询实体</param>
        /// <param name="strWhere">where条件</param>
        /// <param name="order">排序</param>
        /// <param name="paras">查询参数</param>
        /// <param name="begin">记录开始索引（以1开始）</param>
        /// <param name="end">记录结束索引（以1开始）</param>
        /// <returns>List[T]</returns>
        public static List<T> Query<T>(this T entity, string strWhere, string order, List<SqlParameter> paras, int begin, int count) where T : new()
        {
            Type t = typeof(T);
            string name = t.Name.FormatChange();
            string strSql = @"select * from (select ROW_NUMBER() over(order by " + order + @") rn,v.* from " + name + @" v where " + strWhere + @") v0 where rn >" + begin + @" and rn <= " + (begin+count);
            //数据库连接KEY
            string strConn = Communal.GetProValue(entity, "Conn");
            //搜索出的数据集
            DataTable dt = SqlHelper.DataSet(strSql, SqlHelper.CreateConn(strConn), paras);
            List<T> listEntity = DbOperExtend.DataTableToList<T>(dt);
            return listEntity;
        }

        /// <summary>
        /// 返回List[T]
        /// </summary>
        /// <param name="entity">查询实体</param>
        /// <param name="strWhere">where条件</param>
        /// <param name="order">排序</param>
        /// <param name="paras">查询参数</param>
        /// <param name="begin">记录开始索引（以1开始）</param>
        /// <param name="end">记录结束索引（以1开始）</param>
        /// <returns>List[T]</returns>
      //public static List<T> Query1<T>(this T entity, string strWhere, string order, List<SqlParameter> paras, int begin, int count) where T : new()
      //  {
      //      Type t = typeof(T);
      //  string name = t.Name.FormatChange();
      //  string strSql1 = @"select * from (select ROW_NUMBER() over(order by " + order + @") rn,v.* from " + name + @" v where " + strWhere + @") v0 where rn >" + begin + @" and rn <= " + (begin + count);
      //  //数据库连接KEY
      //  string strConn = Communal.GetProValue(entity, "Conn");
      //  //搜索出的数据集
      //  DataTable dt = SqlHelper.DataSet(strSql1, SqlHelper.CreateConn(strConn), paras);
      //  List<T> listEntity = DbOperExtend.DataTableToList<T>(dt);
      //      return listEntity;
      //  }
    /// <summary>
    /// 查询记录总数
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="strWhere">where条件</param>
    /// <param name="paras">查询参数</param>
    /// <returns>记录总数</returns>
    public static int Count<T>(this T entity, string strWhere, List<SqlParameter> paras) where T : new()
        {
            Type t = typeof(T);
            string name = t.Name.FormatChange();
            string strSql = @"select count(1) cnt  from " + name + @" where " + strWhere;
            //数据库连接KEY
            string strConn = Communal.GetProValue(entity, "Conn");
            //记录总数
            int decCnt = int.Parse(SqlHelper.ExecuteScalar(strSql, SqlHelper.CreateConn(strConn), paras));
            return decCnt;
        }

    }
}
