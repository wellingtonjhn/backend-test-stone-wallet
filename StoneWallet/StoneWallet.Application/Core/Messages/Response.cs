using System.Collections.Generic;

namespace StoneWallet.Application.Core.Messages
{
    public class Response
    {
        public IDictionary<string, string> Errors { get; } = new Dictionary<string, string>();
        public object Result { get; }

        public Response(object result)
        {
            Result = result;
        }
    }
}