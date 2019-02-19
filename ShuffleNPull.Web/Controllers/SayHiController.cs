using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams;
using Microsoft.Bot.Connector.Teams.Models;

namespace ShuffleNPull.Web.Controllers
{
    public class SayHiController : ApiController
    {
        //[HttpPost]
        //public async Task<HttpResponseMessage> Post([FromBody] string stringMessage)
        //{
            //var message = Activity.CreateMessageActivity();
            //message.Text = stringMessage;

            //var conversationParameters = new ConversationParameters
            //{
            //    IsGroup = true,
            //    ChannelData = new TeamsChannelData
            //    {
            //        Channel = new ChannelInfo(ConfigurationManager.AppSettings["ChannelId"]),
            //    },
            //    Activity = (Activity)message
            //};

            //var connectorClient = new ConnectorClient(new Uri(ConfigurationManager.AppSettings["ServiceUrl"])
            //    ,microsoftAppId: ConfigurationManager.AppSettings["MicrosoftAppId"]
            //    , microsoftAppPassword: ConfigurationManager.AppSettings["MicrosoftAppPassword"]);
            //MicrosoftAppCredentials.TrustServiceUrl(ConfigurationManager.AppSettings["ServiceUrl"], DateTime.MaxValue);

            //var response = await connectorClient.Conversations.CreateConversationAsync(conversationParameters);

            //return new HttpResponseMessage(HttpStatusCode.Accepted);
        //}
    }
}
