using MediatR;
using Newtonsoft.Json;
using System;

namespace StoneWallet.Application.Events
{
    public abstract class Event<TEvent> : INotification
    {
        [JsonProperty(Order = 1)]
        public string EventName = typeof(TEvent).Name;

        [JsonProperty(Order = 2)]
        public DateTime EventDate { get; } = DateTime.Now;
    }
}