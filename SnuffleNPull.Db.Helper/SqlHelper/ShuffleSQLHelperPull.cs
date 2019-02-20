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
        public static DbPull SqlSavePull(DbPull saveItem)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO dbo.DB_PULL
                (
                    CALLER_NAME,
                    PULLED_NAME,
                    TEAM_NAME,
                    CHANNEL_NAME,
                    PULL_MESSAGE,
                    PULL_DT
                )
                VALUES
                (
                    @CallerName,
                    @PulledName,
                    @TeamName,
                    @ChannelName,
                    @PullMessage,
                    @PullDate
                )", sqlConnection);

                cmd.Parameters.AddWithValue("@CallerName", saveItem.CallerName);
                cmd.Parameters.AddWithValue("@PulledName", saveItem.PulledName);
                cmd.Parameters.AddWithValue("@TeamName", saveItem.TeamName);
                cmd.Parameters.AddWithValue("@ChannelName", saveItem.ChannelName);
                cmd.Parameters.AddWithValue("@PullMessage", saveItem.PullMessage);
                cmd.Parameters.AddWithValue("@PullDate", saveItem.PullDate);

                sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }

            return saveItem;
        }
    }
}
