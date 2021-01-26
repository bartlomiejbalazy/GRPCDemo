using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GRPCServer;

namespace GlobalTemperatureConsumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var globalTemperatureClient = new GlobalTemperature.GlobalTemperatureClient(channel);

            using (var serverStreamingCall = globalTemperatureClient.GetMeasurementsForRegion(new GetMeasurementsRequest {RegionId = 1}))
            {
                while (await serverStreamingCall.ResponseStream.MoveNext(CancellationToken.None))
                {
                    var currentMeasurementModel = serverStreamingCall.ResponseStream.Current;
                    Console.WriteLine($"RegionId : { currentMeasurementModel.RegionId } Current temperature: { currentMeasurementModel.Temperature }C");
                }
            }
            Console.ReadLine();
        }
    }
}
