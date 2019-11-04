using System;
using System.Threading.Tasks;
using ElleRealTime.Shared.Test;
using ElleRealTime.Shared.Test.Interfaces;
using Grpc.Core;
using MagicOnion.Client;

namespace TestClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = new Channel("localhost", 12345, ChannelCredentials.Insecure);

            var client = MagicOnionClient.Create<IMyFirstService>(channel);

            var result = await client.SumAsync(100, 200);
            Console.WriteLine("Client received: " + result);
        }
    }
}
