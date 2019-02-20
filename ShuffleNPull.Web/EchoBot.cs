using System;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams;
using Microsoft.Bot.Connector.Teams.Models;

namespace ShuffleNPull.Web
{
    public class EchoBot
    {
        public static async Task EchoMessage(ConnectorClient connector, Activity activity)
        {
            if (activity.Type.Equals("message"))
            {
                var members = await connector.Conversations.GetConversationMembersAsync(activity.Conversation.Id);
                var channelThing = activity.GetChannelData<TeamsChannelData>();
                var teamInfo = connector.GetTeamsConnectorClient().Teams.FetchTeamDetails(channelThing.Team.Id);
                var convoList = connector.GetTeamsConnectorClient().Teams.FetchChannelList(channelThing.Team.Id);
                //var reply = activity.CreateReply($"The members are {string.Join("\n\n", members.Select(x => x.Name))}");

                var reply = activity.CreateReply($"Shuffles Deck... Pulls Card\n\n");

                Random rnd = new Random();
                int poorSoul = rnd.Next(members.Count);

                //reply.AddMentionToText(members[poorSoul], MentionTextLocation.AppendText);

                reply.Text += $"\n\nCaller Name: {activity.From.Name} \n\nPulledName: {members[poorSoul].Name} \n\nTeam Name: {teamInfo.Name} \n\nChannel Name: {convoList.Conversations.First(x => x.Id.Equals(channelThing.Channel.Id)).Name} \n\nPull Message: {activity.GetTextWithoutMentions()} \n\nPull Date: {DateTime.Now}";
                reply.Text += $"\n\nChannel Id: {channelThing.Channel.Id}";
                reply.Text += $"\n\nConvo List First Id: {convoList.Conversations[0].Id}";
                reply.Text += $"\n\nConvo List First Name: {convoList.Conversations[0].Name}";

                await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);
            }
        }
    }
}
