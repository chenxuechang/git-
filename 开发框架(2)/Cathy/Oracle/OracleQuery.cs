﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Cathy.Oracle
{
    public static class OracleQuery
    {
        /// <summary>
        /// 返回List[T]
        /// </summary>
        /// <param name="entity">查询实体</param>
        /// <param name="strWhere">where条件</param>
        /// <param name="order">排序</param>
        /// <param name="paras">查询参数</param>
        /// <param name="begin">记录开始索引（以0开始）</param>
        /// <param name="count">要取的记录总条数</param>
        /// <returns>List[T]</returns>
        public static List<T> Query<T>(this T entity, string strWhere, string order, List<OracleParameter> paras, int begin, int count) where T : new()
        {
            Type t = typeof(T);
            string name = t.Name.FormatChange();
            string strSql = @"select *
                                      from (select t1.*, rownum rn
                                              from (select t.*
                                                      from "+name+@" t
                                                     where "+strWhere+@"
                                                     order by "+order+@") t1)
                                     where rn > "+begin+" and rn <= "+(begin+count);
            //数据库连接KEY
            string strConn = Communal.GetProValue(entity, "Conn");
            //搜索出的数据集
            DataTable dt = OracleDbOper.DataSet(strSql, OracleDbOper.CreateConn(strConn), paras);
            List<T> listEntity = DbOperExtend.DataTableToList<T>(dt);
            return listEntity;
        }

        /// <summary>
        /// 查询记录总数
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="strWhere">where条件</param>
        /// <param name="paras">查询参数</param>
        /// <returns>记录总数</returns>
        public static int Count<T>(this T entity, string strWhere, List<OracleParameter> paras) where T : new()
        {
            Type t = typeof(T);
            string name = t.Name.FormatChange();
            string strSql = @"select count(1) cnt  from " + name + @" where " + strWhere;
            //数据库连接KEY
            string strConn = Communal.GetProValue(entity, "Conn");
            //记录总数
            int decCnt = int.Parse(OracleDbOper.ExecuteScalar(strSql, OracleDbOper.CreateConn(strConn), paras));
            return decCnt;
        }
    }
}
