namespace PocNServiceBus.Subscriber
{
    using System;
    using NServiceBus;
    using Events;

    public class CreatedPersonEventHandler : IHandleMessages<CreatedPersonEvent>
    {
        public void Handle(CreatedPersonEvent message)
        {
            Console.WriteLine($"Persona recibida: {message.Name}");
        }
    }
}
