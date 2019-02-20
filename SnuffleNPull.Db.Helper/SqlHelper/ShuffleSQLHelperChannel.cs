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
        public static DbChannel SqlGetSingleChannel(string id)
        {
            DbChannel rtnItem = null;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM dbo.DB_CHANNEL WHERE CHANNEL_ID = @ChannelId", sqlConnection);

                cmd.Parameters.AddWithValue("@ChannelId", id);

                sqlConnection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rtnItem = new DbChannel();

                        rtnItem.ChannelId = reader["CHANNEL_ID"].ToString();
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
                    CHANNEL_NAME
                )
                VALUES
                (
                    @ChannelId,
                    @ChannelName
                )", sqlConnection);

                cmd.Parameters.AddWithValue("@ChannelId", saveItem.ChannelId);
                cmd.Parameters.AddWithValue("@ChannelName", saveItem.ChannelName);

                sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }

            return saveItem;
        }
    }
}
