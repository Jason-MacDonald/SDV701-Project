﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace SelfHost
{
    static class clsDbConnection
    {
        #region VARIABLES
        
        private static ConnectionStringSettings ConnectionStringSettings = ConfigurationManager.ConnectionStrings["ElectrifyDatabase"];
        private static DbProviderFactory ProviderFactory = DbProviderFactories.GetFactory(ConnectionStringSettings.ProviderName);
        private static string ConnectionStr = ConnectionStringSettings.ConnectionString;
        #endregion

        public static DataTable GetDataTable(string prSQL, Dictionary<string, Object> prPars)
        {
            using (DataTable lcDataTable = new DataTable("TheTable"))
            using (DbConnection lcDataConnection = ProviderFactory.CreateConnection())
            using (DbCommand lcCommand = lcDataConnection.CreateCommand())
            {
                lcDataConnection.ConnectionString = ConnectionStr;
                lcDataConnection.Open();
                lcCommand.CommandText = prSQL;
                SetPars(lcCommand, prPars);
                using (DbDataReader lcDataReader = lcCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    lcDataTable.Load(lcDataReader);
                return lcDataTable;
            }
        }
        public static int Execute(string prSQL, Dictionary<string, Object> prPars)
        {
            using (DbConnection lcDataConnection = ProviderFactory.CreateConnection())
            using (DbCommand lcCommand = lcDataConnection.CreateCommand())
            {
                lcDataConnection.ConnectionString = ConnectionStr;
                lcDataConnection.Open();
                lcCommand.CommandText = prSQL;
                SetPars(lcCommand, prPars);
                return lcCommand.ExecuteNonQuery();
            }
        }

        private static void SetPars(DbCommand prCommand, Dictionary<string, Object> prPars)
        { 
            if (prPars != null)
            {         
                foreach (KeyValuePair<string, Object> lcItem in prPars)
                {
                    DbParameter lcPar = ProviderFactory.CreateParameter();
                    lcPar.Value = lcItem.Value == null ? DBNull.Value : lcItem.Value;
                    lcPar.ParameterName = '@' + lcItem.Key;
                    prCommand.Parameters.Add(lcPar);
                }
            }
        }
    }
}
