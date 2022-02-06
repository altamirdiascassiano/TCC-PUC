using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Threading.Tasks;

namespace gisa.comum
{
    public class AgenteSQS
    {
        string _idConnection;
        string _secretKey;
        string _urlQueue;
        public AgenteSQS(string idConnection, string secretKey, string urlQueue)
        {
            _idConnection = idConnection;
            _secretKey = secretKey;
            _urlQueue = urlQueue;
        }

        public async Task SalvaNoEventBus(string jsonObject)
        {
            try
            {
                var awsCreds = new BasicAWSCredentials(_idConnection, _secretKey);
                var amazonSQSClient = new AmazonSQSClient(awsCreds, Amazon.RegionEndpoint.USEast2);
                var sendRequest = new SendMessageRequest();
                sendRequest.QueueUrl = _urlQueue;
                sendRequest.MessageBody = jsonObject;
                var sendMessageResponse = amazonSQSClient.SendMessageAsync(sendRequest).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
