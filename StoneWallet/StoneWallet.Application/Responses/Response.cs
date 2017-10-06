using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StoneWallet.Application.Responses
{
    public class Response
    {
        private readonly IList<string> _errors = new List<string>();

        public IEnumerable<string> Errors { get; }
        public object Result { get; }

        public Response()
        {
            Errors = new ReadOnlyCollection<string>(_errors);
        }

        public Response(object result)
            : this()
        {
            Result = result;
        }

        public Response AddError(string error)
        {
            _errors.Add(error);
            return this;
        }
    }
}