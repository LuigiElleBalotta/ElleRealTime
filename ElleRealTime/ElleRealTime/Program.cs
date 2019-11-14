using System;
using System.Data.Common;
using System.Data.SqlClient;
using ElleFramework.Utils;
using ElleRealTime.Core.Configuration;
using ElleRealTime.Core.Logging;
using ElleRealTime.Shared.BO;
using Grpc.Core;
using MagicOnion.Server;
using MySql.Data.MySqlClient;

namespace ElleRealTime
{
    class Program
    {
        public static Logger Logger { get; set; }
        static void Main(string[] args)
        {
            Logger = new Logger();
            GrpcEnvironment.SetLogger(new Grpc.Core.Logging.ConsoleLogger());

            var service = MagicOnionEngine.BuildServerServiceDefinition(isReturnExceptionStackTraceInErrorDetail: true);

            /*LOAD CONFIGURATION*/
            Configuration.ReadConfiguration();
            DbProviderFactories.RegisterFactory("MySql.Data.MySqlClient", MySqlClientFactory.Instance);
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
            Shared.BO.Utils.ElleRealTimeDB = new DB(ApplicationUtils.Configuration.Database.Type, ApplicationUtils.Configuration);



            var port = new ServerPort("localhost", 12345, ServerCredentials.Insecure);

            var server = new global::Grpc.Core.Server
            {
                Services = {service}
            };

            server.Ports.Add(port);

            server.Start();

            string line = "";
            do
            {
                line = Console.ReadLine();
                if (line != "quit")
                {
                    Logger.Success(line);
                }

            } while (line != "quit");

        }
    }
}
