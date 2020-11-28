using System;
using System.Threading.Tasks;
using Grpc.Core;
using MeterReaderWeb.Data;
using MeterReaderWeb.Data.Entities;
using Microsoft.Extensions.Logging;

namespace MeterReaderWeb.Services
{
    public class MeterService :  MeterReadingService.MeterReadingServiceBase
    {
        private readonly ILogger _logger;
        private readonly IReadingRepository _repository;

        public MeterService(
            ILogger logger,
            IReadingRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public override async Task<StatusMessage> AddReading(ReadingPacket request, ServerCallContext context)
        {
            var result = new StatusMessage {
                Success = ReadingStatus.Success
            };

            if (request.Successful == ReadingStatus.Success)
            {
                try
                {
                    foreach(var reading in request.Readings)
                    {
                        var data = new MeterReading
                        {
                            Value = reading.ReadingValue,
                            ReadingDate = reading.ReadingTime.ToDateTime(),
                            CustomerId = reading.CustomerId
                        };

                        _repository.AddEntity(data);
                    }

                    if (await _repository.SaveAllAsync())
                    {
                        result.Success = ReadingStatus.Success;
                    }
                }
                catch (Exception ex)
                {
                    result.Message = "Exception being thrown during process";
                    _logger.LogError($"Exception thrown during saving of readings: {ex}");
                }
            }

            return result;
        }
    }
}
