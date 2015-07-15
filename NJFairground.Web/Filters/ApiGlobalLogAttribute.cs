

namespace NJFairground.Web.Filters
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using NJFairground.Web.Utilities;

    public abstract class MessageHandler : DelegatingHandler
    {
        /// <summary>
        /// Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
        /// </summary>
        /// <param name="request">The HTTP request message to send to the server.</param>
        /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task`1" />. The task object representing the asynchronous operation.
        /// </returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var corrId = string.Format("{0}{1}", DateTime.Now.Ticks, Thread.CurrentThread.ManagedThreadId);
            var requestInfo = string.Format("{0} {1}", request.Method, request.RequestUri);

            var requestMessage = await request.Content.ReadAsByteArrayAsync();

            await IncommingMessageAsync(corrId, requestInfo, requestMessage);

            var response = await base.SendAsync(request, cancellationToken);

            byte[] responseMessage;

            if (response.IsSuccessStatusCode)
                responseMessage = await response.Content.ReadAsByteArrayAsync();
            else
                responseMessage = Encoding.UTF8.GetBytes(response.ReasonPhrase);

            await OutgoingMessageAsync(corrId, requestInfo, responseMessage);

            return response;
        }

        /// <summary>
        /// Incommings the message asynchronous.
        /// </summary>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="requestInfo">The request information.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        protected abstract Task IncommingMessageAsync(string correlationId, string requestInfo, byte[] message);

        /// <summary>
        /// Outgoings the message asynchronous.
        /// </summary>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="requestInfo">The request information.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        protected abstract Task OutgoingMessageAsync(string correlationId, string requestInfo, byte[] message);
    }

    public class MessageLoggingHandler : MessageHandler
    {
        private readonly string logFileName = "";
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageLoggingHandler"/> class.
        /// </summary>
        public MessageLoggingHandler()
        {
            logFileName = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// Incommings the message asynchronous.
        /// </summary>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="requestInfo">The request information.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        protected override async Task IncommingMessageAsync(string correlationId, string requestInfo, byte[] message)
        {
            if (CommonUtility.GetAppSetting<bool>("LogRequestResponse"))
            {
                await Task.Run(() =>
                    ThreadPool.QueueUserWorkItem((state) =>
                        CommonUtility.LogToFileWithStack(string.Format("{0} - Request: {1}\r\n{2}",
                        correlationId, requestInfo, Newtonsoft.Json.Linq.JObject.Parse(Encoding.UTF8.GetString(message)).ToString()), logFileName)
                    )
                ); 
            }
        }

        /// <summary>
        /// Outgoings the message asynchronous.
        /// </summary>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="requestInfo">The request information.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        protected override async Task OutgoingMessageAsync(string correlationId, string requestInfo, byte[] message)
        {
            if (CommonUtility.GetAppSetting<bool>("LogRequestResponse"))
            {
                await Task.Run(() =>
                    ThreadPool.QueueUserWorkItem((state) =>
                        CommonUtility.LogToFileWithStack(string.Format("{0} - Response: {1}\r\n{2}",
                        correlationId, requestInfo, Newtonsoft.Json.Linq.JObject.Parse(Encoding.UTF8.GetString(message)).ToString()), logFileName)
                    )
                );
            }
        }
    }
}