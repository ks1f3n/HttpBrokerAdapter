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
using SqlAdapterCore.Model;
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

                if (createGapSensorDto.v != null)
                {
                    Gap.Value = createGapSensorDto.v[0];
                    _logger.LogInformation("D: {0} D0: {1}", createGapSensorDto.v[0], Gap.InitValue);
                    var info = new SensorInfo(createGapSensorDto.PER, createGapSensorDto.VOLT, createGapSensorDto.CSQ);
                    var meas = createGapSensorDto.v.Select((d, i) => new GapSensorMeas(d, createGapSensorDto.t[i], Gap.InitValue));
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
                }
                var sper = Gap.Period > 0 ? Gap.Period : 120;
                var mper = sper / 20;
                return Ok(String.Format("PER={0},TSP={1},ENC={2},", sper, unixDateTime, mper));
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

                if (createInclSensorDto.X!=null)
                {
                    Incl.X = createInclSensorDto.X[0];
                    Incl.Y = createInclSensorDto.Y[0];
                    _logger.LogInformation("X: {0}; Y: {1}; X0: {0}; Y0: {1}", createInclSensorDto.X[0], createInclSensorDto.Y[0], Gap.InitX, Incl.InitY);
                    var info = new SensorInfo(createInclSensorDto.PER, createInclSensorDto.VOLT, createInclSensorDto.CSQ);
                    var meas = createInclSensorDto.X.Select((x, i) => new InclSensorMeas(x, createInclSensorDto.Y[i], createInclSensorDto.T[i], createInclSensorDto.TS[i], Gap.InitX, Incl.InitY));
                    var msg = new Message(info, meas);

                    var str = JsonConvert.SerializeObject(msg);

                    var message = new MqttApplicationMessageBuilder()
                        .WithTopic("legacy/incl/" + createInclSensorDto.UID)
                        .WithPayload(str)
                        .WithAtMostOnceQoS()
                        .Build();

                    await _mqttClient.PublishAsync(message);
                }                

                var dateNow = DateTime.Now;
                var dateTimeOffset = new DateTimeOffset(dateNow);
                var unixDateTime = dateTimeOffset.ToUnixTimeSeconds();

                var sper = Incl.Period > 0 ? Incl.Period : 120;
                var mper = sper / 20;
                return Ok(String.Format("UID={0}, ST={1}, TS={2}, SPER={3}, MPER={4}, IGVAL={5},CRVAL={6},", createInclSensorDto.UID, createInclSensorDto.ST, unixDateTime, sper, mper, 2, 150));
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