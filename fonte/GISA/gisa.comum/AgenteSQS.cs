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
        public AgenteSQS(string idConnection, string secretKey,string urlQueue)
        {
            this._idConnection = idConnection;
            this._secretKey = secretKey;
            this._urlQueue = urlQueue;
        }

        public async Task SalvaNoEventBus( string jsonObject)
        {
            try
            {
            var awsCreds = new BasicAWSCredentials(_idConnection, _secretKey);
            var amazonSQSClient = new AmazonSQSClient(awsCreds, Amazon.RegionEndpoint.USEast2);
            var sendRequest = new SendMessageRequest();
            sendRequest.QueueUrl = _urlQueue;
            sendRequest.MessageBody = jsonObject;
            var sendMessageResponse = amazonSQSClient.SendMessageAsync(sendRequest).Result;            
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    #region https://www.c-sharpcorner.com/article/how-to-implement-amazon-sqs-aws-sqs-in-asp-net-core-project/ 
    /*
    public interface IAgenteSQS
    {
        Task<bool> SendMessageAsync(UserDetail userDetail);
        Task<List<Message>> ReceiveMessageAsync();
        Task<bool> DeleteMessageAsync(string messageReceiptHandle);
    }

    class AgenteSQS
    {
        private readonly IAmazonSQS _sqs;
        private readonly AwsSqsServiceConfiguration _settings;
        public AgenteSQS(
           IAmazonSQS sqs,
           IOptions<AwsSqsServiceConfiguration> settings)
        {
            this._sqs = sqs;
            this._settings = settings.Value;
        }

        public async Task<bool> SendMessageAsync(UserDetail userDetail)
        {
            try
            {
                string message = JsonConvert.SerializeObject(userDetail);
                var sendRequest = new SendMessageRequest(_settings.AWSSQS.QueueUrl, message);
                // Post message or payload to queue  
                var sendResult = await _sqs.SendMessageAsync(sendRequest);

                return sendResult.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Message>> ReceiveMessageAsync()
        {
            try
            {
                //Create New instance  
                var request = new ReceiveMessageRequest
                {
                    QueueUrl = _settings.AWSSQS.QueueUrl,
                    MaxNumberOfMessages = 10,
                    WaitTimeSeconds = 5
                };
                //CheckIs there any new message available to process  
                var result = await _sqs.ReceiveMessageAsync(request);

                return result.Messages.Any() ? result.Messages : new List<Message>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteMessageAsync(string messageReceiptHandle)
        {
            try
            {
                //Deletes the specified message from the specified queue  
                var deleteResult = await _sqs.DeleteMessageAsync(_settings.AWSSQS.QueueUrl, messageReceiptHandle);
                return deleteResult.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    */
    #endregion
}
