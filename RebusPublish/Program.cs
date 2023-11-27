using Rebus.Activation;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Logging;
using Rebus.Persistence.FileSystem;
using Rebus.Persistence.InMem;
using Rebus.PostgreSql;
using Rebus.Routing.TypeBased;
using Rebus.Transport.InMem;
using SharedKernel;
using SharedKernel.Models;
using System;

namespace RebusPublish;

internal class Program
{
    private static string name = "publisher1";

    static void Main(string[] args)
    {
		try
		{
            var cp = new PostgresConnectionHelper(QueueConstants.ConnectionString);
            var transportOptions = new PostgresConnectionHelper(QueueConstants.ConnectionString);


            using (var activator = new BuiltinHandlerActivator())
            {
                Configure
                .With(activator)
                //.OneWayClient()
                .Logging(l => l.ColoredConsole(minLevel: LogLevel.Warn))
                //.Transport(t => t.UsePostgreSqlAsOneWayClient(QueueConstants.ConnectionString, "tomesmb", TimeSpan.FromMinutes(10), QueueConstants.SchemaName))
                .Transport(t => t.UsePostgreSql(QueueConstants.ConnectionString, "tomesmb", "et1", TimeSpan.FromMinutes(10), QueueConstants.SchemaName))
                .Subscriptions(s => s.StoreInPostgres(cp, "Subscriptions", true, true, QueueConstants.SchemaName))
                .Start();


                var message = new SentEmailConfirmation(Guid.NewGuid(), "eugenetomes@gmail.com", $"messageProcessedAt {DateTime.Now}");


                activator.Bus.Subscribe<TestModel>().Wait();
                activator.Bus.Publish(message);
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
}
