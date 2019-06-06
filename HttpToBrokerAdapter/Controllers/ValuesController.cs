using HttpToBrokerAdapter.Dtos;
using HttpToBrokerAdapter.Models;
using HttpToBrokerAdapter.Models.Gap;
using HttpToBrokerAdapter.Models.Incl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HttpToBrokerAdapter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly IMqttClient _mqttClient;
        private readonly IMqttClientOptions _clientOptions;

        public ValuesController(IMqttClient mqttClient, IMqttClientOptions clientOptions, ILogger<ValuesController> logger)
        {
            _mqttClient = mqttClient;
            _clientOptions = clientOptions;
            _logger = logger;
        }

        [HttpPost("Gap/{id}")]
        public async Task<IActionResult> CreateGapAsync([FromForm] CreateGapSensorDto createGapSensorDto)
        {
            try
            {
                if (!_mqttClient.IsConnected)
                    await _mqttClient.ConnectAsync(_clientOptions);

                var info = new SensorInfo(createGapSensorDto.PER, createGapSensorDto.VOLT, createGapSensorDto.CSQ);
                var meas = createGapSensorDto.v.Select((d, i) => new GapSensorMeas(d, createGapSensorDto.t[i]));
                var msg = new Message(info, meas);

                var str = JsonConvert.SerializeObject(msg);

                var message = new MqttApplicationMessageBuilder()
                    .WithTopic("legacy/gap/" + createGapSensorDto.ID)
                    .WithPayload(str)
                    .WithAtMostOnceQoS()
                    .Build();

                await _mqttClient.PublishAsync(message);

                var dateNow = DateTime.Now;
                var dateTimeOffset = new DateTimeOffset(dateNow);
                var unixDateTime = dateTimeOffset.ToUnixTimeSeconds();

                return Ok(String.Format("PER={0},TSP={1},ENC={2},", 120, unixDateTime, 6));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            await _mqttClient.DisconnectAsync();
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("Incl/{id}")]
        public async Task<IActionResult> CreateInclAsync([FromForm] CreateInclSensorDto createInclSensorDto)
        {
            try
            {
                if (!_mqttClient.IsConnected)
                    await _mqttClient.ConnectAsync(_clientOptions);

                var info = new SensorInfo(createInclSensorDto.PER, createInclSensorDto.VOLT, createInclSensorDto.CSQ);
                var meas = createInclSensorDto.X.Select((x, i) => new InclSensorMeas(x, createInclSensorDto.Y[i], createInclSensorDto.T[i], createInclSensorDto.TS[i]));
                var msg = new Message(info, meas);

                var str = JsonConvert.SerializeObject(msg);

                var message = new MqttApplicationMessageBuilder()
                    .WithTopic("legacy/incl/" + createInclSensorDto.UID)
                    .WithPayload(str)
                    .WithAtMostOnceQoS()
                    .Build();

                await _mqttClient.PublishAsync(message);

                var dateNow = DateTime.Now;
                var dateTimeOffset = new DateTimeOffset(dateNow);
                var unixDateTime = dateTimeOffset.ToUnixTimeSeconds();

                return Ok(String.Format("UID={0}, ST={1}, TS={2}, SPER={3}, MPER={4}, IGVAL={5},CRVAL={6},", createInclSensorDto.UID, createInclSensorDto.ST, unixDateTime, 120, 10, 2, 150));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            await _mqttClient.DisconnectAsync();
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}