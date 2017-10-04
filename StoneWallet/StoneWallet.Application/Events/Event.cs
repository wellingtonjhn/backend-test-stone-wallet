using MediatR;
using System;

namespace StoneWallet.Application.Events
{
    public abstract class Event<TEvent> : INotification
    {
        public string EventName = typeof(TEvent).Name;
        public DateTime EventDate { get; } = DateTime.Now;
    }
}