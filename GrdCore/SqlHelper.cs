using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrdCore
{
    public class SqlHelper
    {
        private SqlConnection cn;

        public SqlHelper(string connectionString)
        {
            cn = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Mo ket noi
        /// </summary>
        private void OpenConnection()
        {
            if (cn.State == ConnectionState.Closed)
                cn.Open();
        }

        /// <summary>
        /// Dong ket noi
        /// </summary>
        private void CloseConnection()
        {
            if (cn.State == ConnectionState.Open)
                cn.Close();
        }

        /// <summary>
        /// Kiem tra ket noi
        /// </summary>
        /// <returns></returns>
        public bool CheckConnection()
        {
            try
            {
                OpenConnection();
                if (cn.State == ConnectionState.Open)
                    return true;
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                CloseConnection();
            }
            return false;
        }
    }
}
