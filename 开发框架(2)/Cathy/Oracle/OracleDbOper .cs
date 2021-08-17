using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Cathy.Oracle
{
    public class OracleDbOper
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        /// <returns>默认的数据库连接[Connection]</returns>
        public static OracleConnection CreateConn()
        {
            string strConn = ConfigHelper.GetSection("Default").GetSection("ConnectionString").Value; 
            OracleConnection conn = new OracleConnection(strConn);
            return conn;
        }

        /// <summary>
        /// 数据库连接
        /// </summary>
        /// <param name="connName">web.config配置的连接名称</param>
        /// <returns>指定的数据库连接</returns>
        public static OracleConnection CreateConn(string connName)
        {
            string strConn = ConfigHelper.GetSection(connName).GetSection("ConnectionString").Value;
            OracleConnection conn = new OracleConnection(strConn);
            return conn;
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <param name="tran">事务</param>
        public static void RollBack(OracleTransaction tran)
        {
            if (tran != null)
            {
                tran.Rollback();
                tran = null;
            }
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="tran">事务</param>
        public static void Commit(OracleTransaction tran)
        {
            if (tran != null)
            {
                tran.Commit();
                tran = null;
            }
        }

        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <param name="conn">数据库连接</param>
        public static void Open(OracleConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        /// <summary>
        /// 关闭数据库
        /// </summary>
        /// <param name="conn">数据库连接</param>
        public static void Close(OracleConnection conn)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 数据阅读器  需手动关闭数据库、dataReader
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>DataReader</returns>
        public static OracleDataReader DataReader(string strSql, OracleConnection conn)
        {
            //打开数据库
            Open(conn);
            //取数据命令
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        /// <summary>
        /// 数据阅读器  无数据库开、关
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>DataReader</returns>
        public static OracleDataReader DataReaderNoOpenClose(string strSql, OracleConnection conn)
        {
            //取数据命令
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        /// <summary>
        /// 数据阅读器  需手动关闭数据库、dataReader
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="paras">sqlparameter参数</param>
        /// <returns>DataReader</returns>
        public static OracleDataReader DataReader(string strSql, OracleConnection conn, List<OracleParameter> paras)
        {
            //打开数据库
            Open(conn);
            //取数据命令
            OracleCommand cmd = new OracleCommand(strSql, conn);
            if (paras != null)
            {
                foreach (OracleParameter para in paras)
                {
                    cmd.Parameters.Add(para);
                }
            }
            OracleDataReader dr = cmd.ExecuteReader();
            cmd.Parameters.Clear();
            return dr;
        }

        /// <summary>
        /// 数据阅读器  无数据库开、关
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="paras">sqlparameter参数</param>
        /// <returns>DataReader</returns>
        public static OracleDataReader DataReaderNoOpenClose(string strSql, OracleConnection conn, List<OracleParameter> paras)
        {
            //取数据命令
            OracleCommand cmd = new OracleCommand(strSql, conn);
            if (paras != null)
            {
                foreach (OracleParameter para in paras)
                {
                    cmd.Parameters.Add(para);
                }
            }
            OracleDataReader dr = cmd.ExecuteReader();
            cmd.Parameters.Clear();
            return dr;
        }

        /// <summary>
        /// 数据集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>DataTable</returns>
        public static DataTable DataSet(string sql, OracleConnection conn)
        {
            OracleDataAdapter sda = new OracleDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            sda.Fill(ds, "table");
            return ds.Tables["table"];
        }

        /// <summary>
        /// 数据集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="paras">sqlparameter参数</param>
        /// <returns>DataTable</returns>
        public static DataTable DataSet(string sql, OracleConnection conn, List<OracleParameter> paras)
        {
            OracleDataAdapter sda = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand(sql, conn);
            sda.SelectCommand = cmd;
            if (paras != null)
            {
                foreach (OracleParameter para in paras)
                {
                    cmd.Parameters.Add(para);
                }
            }
            DataSet ds = new DataSet();
            sda.Fill(ds, "table");
            cmd.Parameters.Clear();

            return ds.Tables["table"];
        }

        /// <summary>
        /// 记录总数
        /// </summary>
        /// <param name="sql">sql语句(select count(1) from tablename where condition)</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>记录总数</returns>
        public static int Count(string sql, OracleConnection conn)
        {
            Open(conn);
            OracleCommand cmd = new OracleCommand(sql, conn);
            int cou = Convert.ToInt32(cmd.ExecuteScalar());
            Close(conn);
            return cou;
        }

        /// <summary>
        /// 记录总数 无数据库开关
        /// </summary>
        /// <param name="sql">sql语句(select count(1) from tablename where condition)</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>记录总数</returns>
        public static int CountNoOpenClose(string sql, OracleConnection conn)
        {
            OracleCommand cmd = new OracleCommand(sql, conn);
            int cou = Convert.ToInt32(cmd.ExecuteScalar());
            return cou;
        }

        /// <summary>
        /// 记录总数
        /// </summary>
        /// <param name="sql">sql语句(select count(1) from tablename where condition)</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="paras">sqlparameter参数</param>
        /// <returns>记录总数</returns>
        public static int Count(string sql, OracleConnection conn, List<OracleParameter> paras)
        {
            Open(conn);
            OracleCommand cmd = new OracleCommand(sql, conn);
            if (paras != null)
            {
                foreach (OracleParameter para in paras)
                {
                    cmd.Parameters.Add(para);
                }
            }
            int cou = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Parameters.Clear();
            Close(conn);
            return cou;
        }

        /// <summary>
        /// 记录总数  无数据库开关
        /// </summary>
        /// <param name="sql">sql语句(select count(1) from tablename where condition)</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="paras">sqlparameter参数</param>
        /// <returns>记录总数</returns>
        public static int CountNoOpenClose(string sql, OracleConnection conn, List<OracleParameter> paras)
        {
            OracleCommand cmd = new OracleCommand(sql, conn);
            if (paras != null)
            {
                foreach (OracleParameter para in paras)
                {
                    cmd.Parameters.Add(para);
                }
            }
            int cou = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Parameters.Clear();
            return cou;
        }


        /// <summary>
        /// 取查询语句中第一个字段值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>查询语句中第一个字段值</returns>
        public static string ExecuteScalar(string sql, OracleConnection conn)
        {
            Open(conn);
            OracleCommand cmd = new OracleCommand(sql, conn);
            string sf = cmd.ExecuteScalar().ToString();
            Close(conn);
            return sf;
        }

        /// <summary>
        /// 取查询语句中第一个字段值 无数据库开关
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>查询语句中第一个字段值</returns>
        public static string ExecuteScalarNoOpenClose(string sql, OracleConnection conn)
        {
            OracleCommand cmd = new OracleCommand(sql, conn);
            string sf = cmd.ExecuteScalar().ToString();
            return sf;
        }

        /// <summary>
        /// 取查询语句中第一个字段值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="paras">sqlparameter参数</param>
        /// <returns>查询语句中第一个字段值</returns>
        public static string ExecuteScalar(string sql, OracleConnection conn, List<OracleParameter> paras)
        {
            Open(conn);
            OracleCommand cmd = new OracleCommand(sql, conn);
            if (paras != null)
            {
                foreach (OracleParameter para in paras)
                {
                    cmd.Parameters.Add(para);
                }
            }
            string sf = cmd.ExecuteScalar().ToString();
            cmd.Parameters.Clear();
            Close(conn);
            return sf;
        }

        /// <summary>
        /// 取查询语句中第一个字段值 无数据库开关
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="paras">sqlparameter参数</param>
        /// <returns>查询语句中第一个字段值</returns>
        public static string ExecuteScalarNoOpenClose(string sql, OracleConnection conn, List<OracleParameter> paras)
        {
            OracleCommand cmd = new OracleCommand(sql, conn);
            if (paras != null)
            {
                foreach (OracleParameter para in paras)
                {
                    cmd.Parameters.Add(para);
                }
            }
            string sf = cmd.ExecuteScalar().ToString();
            cmd.Parameters.Clear();
            return sf;
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>影响数据库的条数</returns>
        public static int ExecuteNonQuery(string sql, OracleConnection conn)
        {
            Open(conn);
            OracleCommand cmd = new OracleCommand(sql, conn);
            int recountNum = cmd.ExecuteNonQuery();
            Close(conn);
            return recountNum;
        }

        /// <summary>
        /// 执行SQL语句  无数据库开关
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>影响数据库的条数</returns>
        public static int ExecuteNonQueryNoOpenClose(string sql, OracleConnection conn)
        {
            OracleCommand cmd = new OracleCommand(sql, conn);
            int recountNum = cmd.ExecuteNonQuery();
            return recountNum;
        }

        /// <summary>
        ///  执行SQL语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="sqlTran">SqlTransaction</param>
        /// <param name="paras">sqlparameter</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQuery(string sql, OracleConnection conn, OracleTransaction sqlTran, List<OracleParameter> paras)
        {
            int recountNum = 0;
            OracleCommand cmd = new OracleCommand(sql, conn);
            if (paras != null)
            {
                foreach (OracleParameter para in paras)
                {
                    cmd.Parameters.Add(para);
                }
            }
            cmd.Transaction = sqlTran;
            recountNum = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return recountNum;
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="paras">sqlparameter参数</param>
        /// <returns>影响数据库的条数</returns>
        public static int ExecuteNonQuery(string sql, OracleConnection conn, List<OracleParameter> paras)
        {
            Open(conn);
            OracleCommand cmd = new OracleCommand(sql, conn);
            if (paras != null)
            {
                foreach (OracleParameter para in paras)
                {
 
                        cmd.Parameters.Add(para);       
                   
                }
            }
            int recountNum = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            Close(conn);
            return recountNum;
        }

        /// <summary>
        /// 执行SQL语句 无数据库开关
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="paras">sqlparameter参数</param>
        /// <returns>影响数据库的条数</returns>
        public static int ExecuteNonQueryNoOpenClose(string sql, OracleConnection conn, List<OracleParameter> paras)
        {
            OracleCommand cmd = new OracleCommand(sql, conn);
            if (paras != null)
            {
                foreach (OracleParameter para in paras)
                {

                    cmd.Parameters.Add(para);

                }
            }
            int recountNum = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return recountNum;
        }

        /// <summary>
        /// 判断记录是否已存在
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>存在:true 不存在:false</returns>
        public static Boolean Exists(string sql, OracleConnection conn)
        {
            DataTable dt = DataSet(sql, conn);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断记录是否已存在
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="paras">sqlparameter参数</param>
        /// <returns>存在:true 不存在:false</returns>
        public static Boolean Exists(string sql, OracleConnection conn, List<OracleParameter> paras)
        {
            DataTable dt = DataSet(sql, conn, paras);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 显示指定字段的值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="fields">指定字段</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>指定字段的值</returns>
        public static string ShowData(string sql, string fields, OracleConnection conn)
        {
            Open(conn);
            OracleCommand cmd = new OracleCommand(sql, conn);
            OracleDataReader dr = cmd.ExecuteReader();
            string zhi = "";
            while (dr.Read())
            {
                zhi = dr[fields].ToString();
            }
            dr.Dispose();
            dr.Close();
            Close(conn);
            return zhi;
        }

        /// <summary>
        /// 显示指定字段的值  无数据库开关
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="fields">指定字段</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>指定字段的值</returns>
        public static string ShowDataNoOpenClose(string sql, string fields, OracleConnection conn)
        {
            OracleCommand cmd = new OracleCommand(sql, conn);
            OracleDataReader dr = cmd.ExecuteReader();
            string zhi = "";
            while (dr.Read())
            {
                zhi = dr[fields].ToString();
            }
            dr.Dispose();
            dr.Close();
            return zhi;
        }

        /// <summary>
        /// 显示指定字段的值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="fields">指定字段</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="paras">sqlparameter参数</param>
        /// <returns>指定字段的值</returns>
        public static string ShowData(string sql, string fields, OracleConnection conn, List<OracleParameter> paras)
        {
            Open(conn);
            OracleCommand cmd = new OracleCommand(sql, conn);
            if (paras != null)
            {
                foreach (OracleParameter para in paras)
                {
                    cmd.Parameters.Add(para);
                }
            }
            OracleDataReader dr = cmd.ExecuteReader();
            cmd.Parameters.Clear();
            string zhi = "";
            while (dr.Read())
            {
                zhi = dr[fields].ToString();
            }
            dr.Dispose();
            dr.Close();
            Close(conn);
            return zhi;
        }

        /// <summary>
        /// 显示指定字段的值  无数据库开关
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="fields">指定字段</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="paras">sqlparameter参数</param>
        /// <returns>指定字段的值</returns>
        public static string ShowDataNoOpenClose(string sql, string fields, OracleConnection conn, List<OracleParameter> paras)
        {
            OracleCommand cmd = new OracleCommand(sql, conn);
            if (paras != null)
            {
                foreach (OracleParameter para in paras)
                {
                    cmd.Parameters.Add(para);
                }
            }
            OracleDataReader dr = cmd.ExecuteReader();
            cmd.Parameters.Clear();
            string zhi = "";
            while (dr.Read())
            {
                zhi = dr[fields].ToString();
            }
            dr.Dispose();
            dr.Close();
            return zhi;
        }
    }
}