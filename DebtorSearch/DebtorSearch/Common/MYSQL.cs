using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace DebtorSearch.Common
{
  
        public sealed class MYSQL
        {
            private MySqlCommand cmd = new MySqlCommand();
            public static string con = String.Empty;

            private MYSQL()
            {
                cmd.CommandTimeout = 0;
                cmd.Connection = new MySqlConnection(con);
            }

            /* private MYSQL()
             {
                 cmd.Connection = new MySqlConnection(ConfigurationManager.AppSettings[dbName]);
             }*/

            public static MYSQL Text(string query)
            {
                MYSQL instance = new MYSQL();
                instance.cmd.Parameters.Clear();
                instance.cmd.CommandText = query;
                instance.cmd.CommandType = CommandType.Text;
                return instance;
            }

            public static MYSQL StoredProcedure(string query)
            {
                MYSQL instance = new MYSQL();
                instance.cmd.Parameters.Clear();
                instance.cmd.CommandText = query;
                instance.cmd.CommandType = CommandType.StoredProcedure;
                return instance;
            }

            public void setQuery(String Query)
            {
                cmd.CommandText = Query;
            }

            public MYSQL AddParameter(string parameterName, MySqlDbType dbType, object value)
            {
                MySqlParameter parameter = new MySqlParameter(parameterName, dbType);
                parameter.Value = value;
                cmd.Parameters.Add(parameter);
                return this;
            }

            public MYSQL OutParameter(string parameterName, MySqlDbType dbType, int size, ref MySqlParameter value)
            {
                MySqlParameter parameter = new MySqlParameter(parameterName, dbType, size);
                parameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parameter);
                value = parameter;
                return this;
            }

            public MYSQL AddParameter(string parameterName, MySqlDbType dbType, int size, object value)
            {
                MySqlParameter parameter = new MySqlParameter(parameterName, dbType, size);
                parameter.Value = value;
                cmd.Parameters.Add(parameter);
                return this;
            }

            public DataTable Query()
            {
                DataTable dt = new DataTable();

                try
                {
                    cmd.Connection.Open();
                    using (MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(cmd))
                    {
                        sqlDataAdapter.Fill(dt);
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (cmd.Connection.State != ConnectionState.Closed)
                    {
                        cmd.Connection.Close();
                    }
                }
                return dt;
            }

            public int NonQuery()
            {
                try
                {
                    cmd.Connection.Open();
                    return cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (cmd.Connection.State != ConnectionState.Closed)
                    {
                        cmd.Connection.Close();
                    }
                }
            }

            public object Scalar()
            {
                try
                {
                    cmd.Connection.Open();
                    return cmd.ExecuteScalar();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (cmd.Connection.State != ConnectionState.Closed)
                    {
                        cmd.Connection.Close();
                    }
                }
            }
        }
    }
