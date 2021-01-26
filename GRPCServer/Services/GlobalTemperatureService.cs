using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GRPCServer
{
    public class GlobalTemperatureService : GlobalTemperature.GlobalTemperatureBase
    {
        private readonly ILogger<GlobalTemperatureService> _logger;
        public GlobalTemperatureService(ILogger<GlobalTemperatureService> logger)
        {
            _logger = logger;
        }

        public override async Task GetMeasurementsForRegion(GetMeasurementsRequest request, IServerStreamWriter<MeasurementsModel> responseStream,
            ServerCallContext context)
        {
            var random = new Random();
            for (int i = 0; i < 50; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                await responseStream.WriteAsync(new MeasurementsModel {RegionId = request.RegionId, Temperature = random.NextDouble()});
            }
        }
    }
}
