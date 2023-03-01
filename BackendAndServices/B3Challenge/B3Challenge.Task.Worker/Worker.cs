using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using static B3Challenge.Rabbit.ReceivedEventArgs;
using B3Challenge.Rabbit;
using AutoMapper;
using System.Text.Json;
using B3Challenge.Business.Interfaces;
using B3Challenge.Business;

namespace B3Challenge.Task.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly int _intervalRabbitConsume;
        private readonly IRabbitConsumer _consumer;
        private readonly string _queueName;
        private readonly ITaskBusiness _taskBusiness;

        public Worker(ILogger<Worker> logger, IMapper mapper, IConfiguration configuration, IRabbitConsumer consumer, ITaskBusiness taskBusiness)
        {
            _configuration= configuration;
            _mapper = mapper;

            _queueName = "operationQueue";
            _intervalRabbitConsume = Convert.ToInt32(_configuration["Rabbit:IntervalRabbitConsume"]);


            _consumer = consumer;// new RabbitConsumer(_configuration["Rabbit:HostName"], _configuration["Rabbit:User"], _configuration["Rabbit:Password"]);
            _consumer.OnReceived += Consumer_OnReceived;

            _taskBusiness = taskBusiness;

            logger.LogInformation(
                $"Queue = {_queueName}");

            _logger = logger;
        }

        protected override async System.Threading.Tasks.Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Aguardando mensagens...");

            _consumer.ConsumeQueue(_queueName);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation(
                    $"Worker ativo em: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                await System.Threading.Tasks.Task.Delay(_intervalRabbitConsume, stoppingToken);
            }
        }

        private void Consumer_OnReceived(object sender, Rabbit.ReceivedEventArgs e)
        {
            var operationMessage = JsonSerializer.Deserialize<OperationMessage>(e.Message);

            if(operationMessage.OperationType == 0) //Insert
            {
                var insertDto = JsonSerializer.Deserialize<Domain.Dtos.Task.TaskInsertDto>(operationMessage.Message);
                _taskBusiness.Insert(_mapper.Map<Domain.Entities.Task>(insertDto));
            }
            else if(operationMessage.OperationType == 1) // Update
            {
                var updateDto = JsonSerializer.Deserialize<Domain.Dtos.Task.TaskUpdateDto>(operationMessage.Message);
           
                var entity = _taskBusiness.GetById(updateDto.Id);
                entity = _mapper.Map<Domain.Entities.Task>(updateDto);

                _taskBusiness.Update(entity);
            }
            else if (operationMessage.OperationType == 2) // Delete
            {
                var taskId = Convert.ToInt32(operationMessage.Message);
                _taskBusiness.Delete(taskId);
            }
            else if (operationMessage.OperationType == 3) // Select
            {

            }


            _logger.LogInformation(
                $"[Nova mensagem | {DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                e.Message);
        }
    }
}
