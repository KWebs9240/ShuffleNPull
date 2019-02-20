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
        public static DbChannel SqlGetSingleChannel(string channelId, string teamId)
        {
            DbChannel rtnItem = null;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM dbo.DB_CHANNEL 
                    WHERE CHANNEL_ID = @ChannelId 
                    AND TEAM_ID = @TeamId", sqlConnection);

                cmd.Parameters.AddWithValue("@ChannelId", channelId);
                cmd.Parameters.AddWithValue("@TeamId", teamId);

                sqlConnection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rtnItem = new DbChannel();

                        rtnItem.ChannelId = reader["CHANNEL_ID"].ToString();
                        rtnItem.TeamId = reader["TEAM_ID"].ToString();
                        rtnItem.ChannelName = reader["CHANNEL_NAME"].ToString();
                    }
                }
            }

            return rtnItem;
        }

        public static DbChannel SqlInsertChannel(DbChannel saveItem)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO dbo.DB_CHANNEL
                (
                    CHANNEL_ID,
                    TEAM_ID,
                    CHANNEL_NAME
                )
                VALUES
                (
                    @ChannelId,
                    @TeamId,
                    @ChannelName
                )", sqlConnection);

                cmd.Parameters.AddWithValue("@ChannelId", saveItem.ChannelId);
                cmd.Parameters.AddWithValue("@TeamId", saveItem.TeamId);
                cmd.Parameters.AddWithValue("@ChannelName", saveItem.ChannelName);

                sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }

            return saveItem;
        }
    }
}
