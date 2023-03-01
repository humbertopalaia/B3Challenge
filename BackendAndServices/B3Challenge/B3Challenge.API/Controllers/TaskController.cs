using B3Challenge.Rabbit;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace B3Challenge.API.Controllers
{
    public class TaskController : Controller
    {

        private ILogger<TaskController> _logger;

        public TaskController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }

        [HttpPut]
        [Route("/api/Insert")]
        public IActionResult Insert([FromBody] Domain.Dtos.Task.TaskInsertDto dto)
        {
            try
            {
                var sender = new RabbitSender("localhost", "guest", "guest");
                var message = new OperationMessage();
                message.MessageId = Guid.NewGuid().ToString();
                message.OperationType = 0;
                message.Message = dto.ToString();

                sender.QueueMessage(message, "operationQueue");
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao tentar inserir tarefa", ex);
                return new StatusCodeResult(500);
            }

            return Accepted();
        }


        [HttpDelete]
        [Route("/api/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var sender = new RabbitSender("localhost", "guest", "guest");
                var message = new OperationMessage();
                message.MessageId = Guid.NewGuid().ToString();
                message.OperationType = 2;
                message.Message = id.ToString();

                sender.QueueMessage(message, "operationQueue");
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao tentar inserir tarefa", ex);
                return new StatusCodeResult(500);
            }

            return Accepted();
        }

        [HttpPost]
        [Route("/api/Update/{id}")]
        public IActionResult Update([FromBody] Domain.Dtos.Task.TaskUpdateDto dto)
        {

            try
            {
                var sender = new RabbitSender("localhost", "guest", "guest");
                var message = new OperationMessage();
                message.MessageId = Guid.NewGuid().ToString();
                message.OperationType = 1;
                message.Message = dto.ToString();

                sender.QueueMessage(message, "operationQueue");
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao tentar inserir tarefa", ex);
                return new StatusCodeResult(500);
            }

            return Accepted();
        }
        public IActionResult Delete()
        {
            return Accepted();

        }
    }
}
