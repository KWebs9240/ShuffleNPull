using System;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams;
using Microsoft.Bot.Connector.Teams.Models;
using ShuffleNPull.Db.Helper.Entities;
using SnuffleNPull.Db.Helper.SqlHelper;

namespace ShuffleNPull.Web
{
    public class EchoBot
    {
        public static async Task EchoMessage(ConnectorClient connector, Activity activity)
        {
            if (activity.Type.Equals("message"))
            {
                var channelThing = activity.GetChannelData<TeamsChannelData>();

                string messagyBoi = activity.GetTextWithoutMentions();
                Activity reply = null;

                //If you get the super secret channel save data command
                if (messagyBoi.Equals("Yo save channel data"))
                {
                    //Check to see whether we've already got it
                    DbChannel existingChannel = ShuffleSQLHelper.SqlGetSingleChannel(channelThing.Channel.Id);
                    if (existingChannel != null)
                    {
                        reply = activity.CreateReply($"Nah fam, you already got data for Channel: {existingChannel.ChannelName}");
                    }
                    else
                    {
                        //If you channel id matches the team id, you're in the autogenerate General
                        if (channelThing.Channel.Id.Equals(channelThing.Team.Id))
                        {
                            var addChannel = new DbChannel()
                            {
                                ChannelId = channelThing.Channel.Id,
                                ChannelName = "General"
                            };

                            ShuffleSQLHelper.SqlInsertChannel(addChannel);
                            reply = activity.CreateReply($"Added data for Channel: {addChannel.ChannelName}");
                        }
                        else
                        {
                            //We gotta get the channels and figure out which one we're in
                            var convoList = connector.GetTeamsConnectorClient().Teams.FetchChannelList(channelThing.Team.Id);
                            var currentChannel = convoList.Conversations.First(x => x.Id.Equals(channelThing.Channel.Id));

                            var addChannel = new DbChannel()
                            {
                                ChannelId = currentChannel.Id,
                                ChannelName = currentChannel.Name
                            };

                            ShuffleSQLHelper.SqlInsertChannel(addChannel);
                            reply = activity.CreateReply($"Added data for Channel: {addChannel.ChannelName}");
                        }
                    }
                }
                else if (messagyBoi.Equals("Yo save team data"))
                {
                    DbTeam existingTeam = ShuffleSQLHelper.SqlGetSingleTeam(channelThing.Team.Id);
                    if (existingTeam != null)
                    {
                        reply = activity.CreateReply($"Nah fam, you already got data for Team: {existingTeam.TeamName}");
                    }
                    else
                    {
                        var teamInfo = connector.GetTeamsConnectorClient().Teams.FetchTeamDetails(channelThing.Team.Id);

                        var addTeam = new DbTeam()
                        {
                            TeamId = teamInfo.Id,
                            TeamName = teamInfo.Name
                        };

                        ShuffleSQLHelper.SqlInsertTeam(addTeam);
                        reply = activity.CreateReply($"Added data for Team: {addTeam.TeamName}");
                    }
                }
                else
                {
                    var members = await connector.Conversations.GetConversationMembersAsync(activity.Conversation.Id);

                    //var reply = activity.CreateReply($"The members are {string.Join("\n\n", members.Select(x => x.Name))}");

                    reply = activity.CreateReply($"Shuffles Deck... Pulls Card\n\n");

                    Random rnd = new Random();
                    int poorSoul = rnd.Next(members.Count);

                    //reply.AddMentionToText(members[poorSoul], MentionTextLocation.AppendText);

                    var newPull = new DbPull()
                    {
                        CallerName = activity.From.Name,
                        PulledName = members[poorSoul].Name,
                        TeamId = channelThing.Team.Id,
                        ChannelId = channelThing.Channel.Id,
                        PullMessage = messagyBoi,
                        PullDate = DateTime.UtcNow
                    };

                    ShuffleSQLHelper.SqlInsertPull(newPull);
                }

                await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);
            }
        }
    }
}
