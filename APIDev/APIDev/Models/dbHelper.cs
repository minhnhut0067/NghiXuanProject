using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace APIDev.Models
{
    public class dbHelper
    {
        public static string ConnectionStringDefault = "Server=172.168.1.248;Port=5432;Database=medisoft_dlsg_111116;User Id=medisoft;Password=links1920;TIMEOUT=20;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=30;";

        public static void convertutf8(string mystring)
        {
            byte[] bytes = Encoding.Default.GetBytes(mystring);
            mystring = Encoding.UTF8.GetString(bytes);
        }

        //private static string ConnectionStringDefault = "Server=localhost;Port=5444;Database=ms_root;User Id=msolution;Password=msolution;TIMEOUT=15;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20;";
        public static DataSet getDataSetbySql(string SqlQuery)
        {
            try
            {
                convertutf8(SqlQuery);
                return getDataSetbySql(SqlQuery, ConnectionStringDefault);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DataSet getDataSetbySql(string SqlQuery, string connstr)
        {
            DataSet ds = new DataSet();
            try
            {
                convertutf8(SqlQuery);
                using (NpgsqlConnection conn = new NpgsqlConnection(connstr))
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand(SqlQuery, conn);

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    conn.Close();
                    return ds;
                }
            }
            catch 
            {
                //ds.Tables[0].Columns.Add("Error");
                //ds.Tables[0].Rows[0]["Error"] = ex.Message;
                return null;
            }
        }

        public static bool ExecuteQuery(string SqlQuery)
        {
            try
            {
                convertutf8(SqlQuery);
                return ExecuteQuery(SqlQuery, ConnectionStringDefault);
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static bool ExecuteQuery(string SqlQuery, string connstr)
        {
            try
            {
                convertutf8(SqlQuery);
                using (NpgsqlConnection conn = new NpgsqlConnection(connstr))
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand(SqlQuery, conn);
                    var i = cmd.ExecuteNonQuery();

                    conn.Close();

                    return i != 0 ? true : false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string getFieldbyWhere(string table, string field, string where)
        {
            try
            {
                convertutf8(table);
                convertutf8(field);
                convertutf8(where);
                return getFieldbyWhere(table, field, where, ConnectionStringDefault);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string getFieldbyWhere(string table, string field, string where, string connstr)
        {
            try
            {
                convertutf8(table);
                convertutf8(field);
                convertutf8(where);
                convertutf8(connstr);
                DataSet ds = new DataSet();
                using (NpgsqlConnection conn = new NpgsqlConnection(connstr))
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand("select " + field + " from " + table + " where " + where + "", conn);

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    conn.Close();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}