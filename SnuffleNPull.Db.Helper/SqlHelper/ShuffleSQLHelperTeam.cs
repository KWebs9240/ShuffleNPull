using ShuffleNPull.Db.Helper.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnuffleNPull.Db.Helper.SqlHelper
{
    public static partial class ShuffleSQLHelper
    {
        public static DbTeam SqlGetSingleTeam(string id)
        {
            DbTeam rtnItem = null;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM dbo.DB_TEAM WHERE TEAM_ID = @TeamId", sqlConnection);

                cmd.Parameters.AddWithValue("@TeamId", id);

                sqlConnection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rtnItem = new DbTeam();

                        rtnItem.TeamId = reader["TEAM_ID"].ToString();
                        rtnItem.TeamName = reader["TEAM_NAME"].ToString();
                    }
                }
            }

            return rtnItem;
        }

        public static DbTeam SqlInsertTeam(DbTeam saveItem)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO dbo.DB_TEAM
                (
                    TEAM_ID,
                    TEAM_NAME
                )
                VALUES
                (
                    @TeamId,
                    @TeamName
                )", sqlConnection);

                cmd.Parameters.AddWithValue("@TeamId", saveItem.TeamId);
                cmd.Parameters.AddWithValue("@TeamName", saveItem.TeamName);

                sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }

            return saveItem;
        }
    }
}
