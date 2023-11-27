using Rebus.Activation;
using Rebus.Config;
using Rebus.Handlers;
using Rebus.Logging;
using Rebus.PostgreSql;
using Rebus.Routing.TypeBased;
using SharedKernel;
using SharedKernel.Models;
using System.Xml.Linq;

namespace Consumer;

internal class Program
{

    private static string name = "consumer1";
    static void Main(string[] args)
    {
        

        try
        {
            var cp = new PostgresConnectionHelper(QueueConstants.ConnectionString);
            using (var activator = new BuiltinHandlerActivator())
            {
                activator.Register(() => new Handler());

                Configure.With(activator)
                    .Logging(l => l.ColoredConsole(minLevel: LogLevel.Warn))
                    .Transport(t => t.UsePostgreSql(QueueConstants.ConnectionString, "tomesmb", name, TimeSpan.FromMinutes(10), QueueConstants.SchemaName))
                    //.Subscriptions(s => s.StoreInPostgres(QueueConstants.ConnectionString, "Subscriptions", isCentralized: true, true))
                    .Subscriptions(s => s.StoreInPostgres(cp, "Subscriptions", true, true, QueueConstants.SchemaName))
                    .Start();

                activator.Bus.Subscribe<SentEmailConfirmation>().Wait();
                

                Console.WriteLine("This is Subscriber 1");
                Console.WriteLine("Press ENTER to quit");
                Console.ReadLine();
                Console.WriteLine("Quitting...");
            }

        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.ToString());
        }
        finally
        {
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }

    class Handler : IHandleMessages<SentEmailConfirmation>
    {
        public async Task Handle(SentEmailConfirmation message)
        {
            Console.WriteLine("Got string: {0} with message: {1}", message.Id, message.Message);
        }
    }
}
