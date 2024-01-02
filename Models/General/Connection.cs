using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HGI.Models.General
{
    public class Connection
    {
        SqlConnection cnn = new SqlConnection();
        SqlCommand cmd;
        SqlDataAdapter adapter;
        SqlTransaction trans;

        internal string _connectionString = "";

        string _error;
        int _errNumber;

        /// <summary>
        /// 
        /// </summary>
        public String error
        {
            get { return _error; }
            set { _error = value; }
        }

        public int errNumber
        {
            get { return _errNumber; }
            set { _errNumber = value; }
        }

        /// <summary>
        /// To make a connection to SQL Server
        /// </summary>
        /// <returns>bool, tru if is connected, false if not.</returns>
        public bool connect()
        {
            bool connected = false;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    //cnn.ConnectionString = ConfigurationManager.ConnectionStrings["DBString"].ConnectionString;
                    cnn.ConnectionString = _connectionString;
                    cnn.Open();
                }
                connected = true;
            }
            catch (SqlException ex)
            {
                _error = ex.Message;
                connected = false;
            }
            catch (InvalidOperationException ex)
            {
                _error = ex.Message;
                connected = false;
            }

            return connected;
        }
        /// </summary>
        /// <returns></returns>
        public bool disconnect()
        {
            bool result = false;

            try
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
                result = true;
            }
            catch (SqlException ex)
            {
                _error = ex.Message;
                result = false;
            }

            return result;
        }
        /// <summary>
        /// procedure to get a DataTable object
        /// </summary>
        /// <param name="sqlStatement"></param>
        /// <returns></returns>
        public DataTable getDataTable(string sqlStatement)
        {
            connect();
            DataTable dt = new DataTable();
            adapter = new SqlDataAdapter(sqlStatement, cnn);
            try
            {
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                disconnect();
                this.error = ex.Message;
                throw new Exception(ex.Message);
            }

            disconnect();
            return dt;
        }
        /// <summary>
        /// procedure to get a DataRow object
        /// </summary>
        /// <param name="sqlStatement"></param>
        /// <returns></returns>
        public DataRow getRow(string sqlStatement)
        {
            connect();
            DataTable dt = new DataTable();
            adapter = new SqlDataAdapter(sqlStatement, cnn);
            //adapter.SelectCommand.Transaction = cnn.BeginTransaction();
            adapter.Fill(dt);
            DataRow row = null;
            if (dt.Rows.Count > 0)
            {
                row = dt.Rows[0];
            }
            disconnect();
            return row;
        }

        public DataRow getRow(string sqlStatement, bool isTransaction)
        {
            connect();
            DataTable dt = new DataTable();
            DataRow row = null;
            adapter = new SqlDataAdapter(sqlStatement, cnn);
            adapter.SelectCommand.Transaction = trans;
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                row = dt.Rows[0];
            }
            disconnect();
            return row;
        }

        /// <summary>
        /// procedure to get a DataSet object
        /// </summary>
        /// <param name="sqlStatement"></param>
        /// <returns></returns>
        public DataSet getDataSet(string sqlStatement, string tablename1, string tablename2)
        {
            connect();
            DataSet ds = new DataSet();
            try
            {
                adapter = new SqlDataAdapter(sqlStatement, cnn);
                adapter.TableMappings.Add("Table", tablename1);
                adapter.TableMappings.Add("Table1", tablename2);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                this.error = ex.ToString();
            }

            disconnect();
            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlStatement"></param>
        /// <returns></returns>
        public bool execute(string sqlStatement)
        {
            try
            {
                connect();
                cmd = new SqlCommand(sqlStatement, cnn);
                cmd.ExecuteNonQuery();
                disconnect();
                return true;
            }
            catch (SqlException ex)
            {
                _error = ex.Message;
                _errNumber = ex.Number;
                return false;
            }
        }
        /// <summary>
        /// Execute a statement, which can be transactional
        /// </summary>
        /// <param name="sqlStatement"></param>
        /// <param name="isTransactional"></param>
        /// <returns></returns>
        public bool execute(string sqlStatement, bool isTransactional)
        {
            try
            {
                connect();

                if (isTransactional)
                {
                    cmd = new SqlCommand(sqlStatement, cnn, trans);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (SqlException ex)
                    {
                        _error = ex.Message;
                        return false;
                        throw;
                    }
                }
                else
                {
                    try
                    {
                        connect();
                        cmd = new SqlCommand(sqlStatement, cnn);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (SqlException ex)
                    {
                        _error = ex.Message;
                        disconnect();
                        return false;
                    }
                }

            }
            catch (SqlException ex)
            {
                _error = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool beginTransaction()
        {
            try
            {
                connect();

                trans = cnn.BeginTransaction();

                return true;
            }
            catch (SqlException ex)
            {
                _error = ex.Message;
                return false;
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool commitTransaction()
        {
            try
            {
                trans.Commit();
                return true;
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                _error = ex.Message;
                return false;
                throw;
            }

        }
        public bool rollbackTransaction()
        {
            try
            {
                trans.Rollback();
                return true;
            }
            catch (SqlException ex)
            {
                _error = ex.Message;
                return false;
                throw;
            }

        }
        /// <summary>
        /// Builder
        /// </summary>
        public Connection()
        {
        }
    }
}