namespace PocNServiceBus.Publisher
{
    using System;
    using NServiceBus;
    using Events;

    class Program
    {
        private static readonly IBus Bus = NServiceBus.Bus
            .Create(GetConfiguration())
            .Start();

        static void Main(string[] args)
        {
            Bus.Publish<CreatedPersonEvent>(x =>
            {
                x.Name = "Juan Perez";
            });
            Console.WriteLine("Evento publicado.");
            Console.ReadKey();
        }

        private static BusConfiguration GetConfiguration()
        {
            var configuration = new BusConfiguration();
            configuration.UseTransport<SqlServerTransport>()
                .DefaultSchema("nsb");
            configuration.UsePersistence<NHibernatePersistence>();

            // Nombre del endpoint, por defecto es el nombre de la dll.
            configuration.EndpointName("pocnservicebus.publisher");

            // Definición de tipos de mensajes.
            configuration.Conventions()
                .DefiningCommandsAs(t => t.Name.EndsWith("Command"))
                .DefiningEventsAs(t => t.Name.EndsWith("Event"))
                .DefiningMessagesAs(t => t.Name.EndsWith("Message"));

            configuration.EnableInstallers();
            return configuration;
        }
    }
}
