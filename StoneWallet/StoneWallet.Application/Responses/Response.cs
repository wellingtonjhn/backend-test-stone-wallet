using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StoneWallet.Application.Responses
{
    /// <summary>
    /// Representa uma resposta para um comando
    /// </summary>
    public class Response 
    {
        private readonly IList<string> _messages = new List<string>();

        public IEnumerable<string> Errors { get; }
        public object Result { get; }

        /// <summary>
        /// Cria uma resposta para um comando
        /// </summary>
        public Response()
        {
            Errors = new ReadOnlyCollection<string>(_messages);
        }

        /// <summary>
        /// Cria uma resposta para um comando
        /// </summary>
        /// <param name="result">Objeto que deverá ser retornado</param>
        public Response(object result)
            : this()
        {
            Result = result;
        }

        /// <summary>
        /// Adiciona uma mensagem de erro na resposta
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Response AddError(string message)
        {
            _messages.Add(message);
            return this;
        }
    }
}