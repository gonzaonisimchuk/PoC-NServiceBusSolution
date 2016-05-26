namespace PocNServiceBus.Subscriber
{
    using NServiceBus;

    /*
		This class configures this endpoint as a Server. More information about 
        how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UseTransport<SqlServerTransport>()
                .DefaultSchema("nsb");
            configuration.UsePersistence<NHibernatePersistence>();

            // Nombre del endpoint, por defecto es el nombre de la dll.
            configuration.EndpointName("pocnservicebus.subscriber");

            // Definición de tipos de mensajes.
            configuration.Conventions()
                .DefiningCommandsAs(t => t.Name.EndsWith("Command"))
                .DefiningEventsAs(t => t.Name.EndsWith("Event"))
                .DefiningMessagesAs(t => t.Name.EndsWith("Message"));

            configuration.EnableInstallers();
        }
    }
}
