using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Libra.Entity
{
    public class ADOBusinessLogic
    {
        private string strConn;
        private bool inTransaction;
        private SqlConnection conn;
        private SqlTransaction sqlTran;
        private SqlCommand cmd;

        public string StrConn
        {
            get { return strConn; }
            set { strConn = value; }
        }

        public ADOBusinessLogic(string StringConexao)
        {
            strConn = StringConexao;
        }

        #region Métodos Privados


        private void OpenConnection()
        {
            if (inTransaction)
                return;

            try
            {
                conn = new SqlConnection(strConn);
                conn.Open();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        private void CloseConnection()
        {
            if (inTransaction)
                return;

            try
            {
                conn.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #endregion

        #region Métodos de Execução de Queries

        public object QueryValue(string sql, object nullValue)
        {
            object result;

            OpenConnection();

            try
            {
                cmd = new SqlCommand(sql, conn);

                if (inTransaction)
                    cmd.Transaction = sqlTran;

                result = cmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    result = nullValue;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CloseConnection();
            }

            return (result);
        }

        public object QueryValue(string sql)
        {
            object result;
            result = QueryValue(sql, null);
            return (result);
        }

        public void QueryFill(ref DataSet ds, string sql)
        {
            SqlDataAdapter da;

            OpenConnection();

            try
            {
                cmd = new SqlCommand(sql, conn);

                if (inTransaction)
                    cmd.Transaction = sqlTran;

                da = new SqlDataAdapter(cmd);

                da.Fill(ds);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CloseConnection();
            }
        }

        //public DataTable QueryFillProcedure(ref DataSet ds, string sql)
        public DataTable QueryFillProcedure(string sql)
        {
            SqlDataReader dr;
            OpenConnection();
            DataTable tabela = new DataTable();
            try
            {
                cmd = new SqlCommand(sql, conn);

                if (inTransaction)
                    cmd.Transaction = sqlTran;

                dr = cmd.ExecuteReader();
                tabela.Load(dr);
                //ds.Tables.Add(tabela);
                return tabela;
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Erro ao executar procedure {0}.\n Mensagem original: {1}", sql, e.Message));
            }
            finally
            {
                CloseConnection();
            }
        }

        public int QueryExec(string sql)
        {
            int result;

            OpenConnection();

            try
            {
                cmd = new SqlCommand(sql, conn);

                if (inTransaction)
                    cmd.Transaction = sqlTran;

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CloseConnection();
            }

            return (result);
        }

        public int QueryExec(string sql, byte[] arquivo)
        {
            int result;

            OpenConnection();

            try
            {
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@BINARIO", SqlDbType.VarBinary).Value = arquivo;

                if (inTransaction)
                    cmd.Transaction = sqlTran;

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CloseConnection();
            }

            return (result);
        }

        public DateTime ServerNow()
        {
            object result;
            string sql = "SELECT GETDATE()";

            OpenConnection();

            try
            {
                cmd = new SqlCommand(sql, conn);

                if (inTransaction)
                    cmd.Transaction = sqlTran;

                result = cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CloseConnection();
            }

            return Convert.ToDateTime(result);
        }

        public DataTable ExecuteReader(string sql)
        {
            OpenConnection();
            var tabela = new DataTable();
            var cmd = new SqlCommand(sql, conn);
            try
            {
                if (inTransaction)
                    cmd.Transaction = sqlTran;
                tabela.Load(cmd.ExecuteReader(CommandBehavior.SingleResult));
                return tabela;
            }
            finally
            {
                CloseConnection();
            }
            throw new NotImplementedException();
        }
        #endregion

        #region Controles de Transação


        public void BeginTransaction()
        {
            try
            {
                OpenConnection();
                sqlTran = conn.BeginTransaction();
                inTransaction = true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void Commit()
        {
            try
            {
                sqlTran.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                inTransaction = false;
                CloseConnection();
            }
        }


        public void Rollback()
        {
            try
            {
                sqlTran.Rollback();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                inTransaction = false;
                CloseConnection();
            }
        }


        #endregion

        private static ConnectionStringSettings GetConnection(string stringName)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[stringName];
        }

        public static String GetConnectionString(string stringName)
        {
            return GetConnection(stringName).ConnectionString;
        }
    }
}
