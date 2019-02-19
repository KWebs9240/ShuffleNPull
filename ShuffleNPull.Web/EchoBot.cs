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
                //var reply = activity.CreateReply($"The members are {string.Join("\n\n", members.Select(x => x.Name))}");

                var reply = activity.CreateReply($"Shuffles Deck... Pulls Card\n\n");

                Random rnd = new Random();
                int poorSoul = rnd.Next(members.Count);

                reply.AddMentionToText(members[poorSoul], MentionTextLocation.AppendText);

                await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);
            }
        }
    }
}
