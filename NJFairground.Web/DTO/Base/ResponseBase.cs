

namespace NJFairground.Web.DTO.Base
{
    public class ResponseBase
    {
        public ResponseBase()
        {
        }

        public ResponseBase(string requestToken)
            : this()
        {
            this.CorrelationToken = requestToken;
        }
        public string ResponseStatus { get; set; }
        public string CorrelationToken { get; set; }
    }
}