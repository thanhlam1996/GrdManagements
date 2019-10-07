using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

namespace GrdCore.DAL
{
    class DACommon
    {
        public static DbParameter CreateInputParameter(DbCommand dbCmd, string prmName, DbType dbType, object value)
        {
            DbParameter dbPrm = dbCmd.CreateParameter();
            dbPrm.ParameterName = prmName;
            dbPrm.DbType = dbType;
            dbPrm.Direction = ParameterDirection.Input;
            dbPrm.Value = value;
            return dbPrm;
        }

        public static DbParameter CreateOutputParameter(DbCommand dbCmd, string prmName, DbType dbType, int size)
        {
            DbParameter dbPrm = dbCmd.CreateParameter();
            dbPrm.ParameterName = prmName;
            dbPrm.DbType = dbType;
            dbPrm.Direction = ParameterDirection.Output;
            dbPrm.Value = DBNull.Value;
            dbPrm.Size = size;
            if (size == 0)
            {
                dbPrm.Size = 255;
            }
            return dbPrm;
        }
    }
}
