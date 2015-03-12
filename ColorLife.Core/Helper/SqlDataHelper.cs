//------------------------------------------------------------------------------
// The contents of this file are subject to the nopCommerce Public License Version 1.0 ("License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at  http://www.nopCommerce.com/License.aspx. 
// 
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. 
// See the License for the specific language governing rights and limitations under the License.
// 
// The Original Code is nopCommerce.
// The Initial Developer of the Original Code is NopSolutions.
// All Rights Reserved.
// 
// Contributor(s): _______. 
//------------------------------------------------------------------------------

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ColorLife.Core.Helper
{
    /// <summary>
    /// Data helper class
    /// </summary>
    public partial class SqlDataHelper
    {
        #region Methods

        internal static string GetConnectionString(string connectionStringName)
        {
            string connectionString = null;

            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (settings != null)
            {
                connectionString = settings.ConnectionString;
            }

            return connectionString;
        }

        /// <summary>
        /// Gets connection string to master database
        /// </summary>
        /// <param name="connetionString">A connection string</param>
        /// <returns></returns>
        public static string GetMasterConnectionString(string connetionString)
        {
            var builder = new SqlConnectionStringBuilder(connetionString);
            builder.InitialCatalog = "master";
            return builder.ToString();
        }

        /// <summary>
        /// Gets database name from connection string
        /// </summary>
        /// <param name="connetionString">A connection string</param>
        /// <returns></returns>
        public static string GetDatabaseName(string connetionString)
        {
            var builder = new SqlConnectionStringBuilder(connetionString);
            return builder.InitialCatalog;
        }

        public static void BackupData(string connectionStringName, string fileName)
        {
            //var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            //var connectionBuilder = new System.Data.SqlClient.SqlConnectionStringBuilder(connectionString);
            //// Console.WriteLine(connectionString.DataSource);
            //// Console.WriteLine(connectionString.InitialCatalog);
            //using (SqlConnection cnn = new SqlConnection(connectionString))
            //{
            //    cnn.Open();
            //    string sqlQuery = "backup database " + connectionBuilder.InitialCatalog + " To DISK = '" + fileName + "' WITH format";
            //    SqlCommand cmd = new SqlCommand(sqlQuery, cnn);
            //    cmd.ExecuteNonQuery();
            //    cnn.Close();
            //}


            using (SqlConnection conn = new SqlConnection(connectionStringName))
            {
                string dbName = GetDatabaseName(connectionStringName);
                string commandText = string.Format(
                    "BACKUP DATABASE [{0}] TO DISK = '{1}' WITH FORMAT",
                    dbName,
                    fileName);

                SqlCommand dbCommand = new SqlCommand(commandText, conn);
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                dbCommand.ExecuteNonQuery();
            }
        }
        #endregion
    }
}
