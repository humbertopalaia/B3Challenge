using B3Challenge.Rabbit;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace B3Challenge.API.Controllers
{
    public class TaskController : Controller
    {

        private ILogger<TaskController> _logger;
        private readonly IConfiguration _configuration;

        public TaskController(ILogger<TaskController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("/api/[controller]/Insert")]
        public IActionResult Insert(Domain.Dtos.Task.TaskInsertDto dto)
        {
            try
            {
                var sender = new RabbitSender(_configuration["Rabbit:Host"], _configuration["Rabbit:User"], _configuration["Rabbit:Password"]);
                var message = new OperationMessage();
                message.MessageId = Guid.NewGuid().ToString();
                message.OperationType = OperationType.Insert;
                message.Message = dto.ToString();
                sender.QueueMessage(message, "operationQueue");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar inserir tarefa");
                return new StatusCodeResult(500);
            }

            return Accepted();
        }


        [HttpDelete]
        [Route("/api/[controller]/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var sender = new RabbitSender(_configuration["Rabbit:Host"], _configuration["Rabbit:User"], _configuration["Rabbit:Password"]);
                var message = new OperationMessage();
                message.MessageId = Guid.NewGuid().ToString();
                message.OperationType = OperationType.Delete;
                message.Message = id.ToString();
                sender.QueueMessage(message, "operationQueue");
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao tentar apagar tarefa", ex);
                return new StatusCodeResult(500);
            }

            return Accepted();
        }

        [HttpPost]
        [Route("/api/[controller]/Update")]
        public IActionResult Update([FromBody] Domain.Dtos.Task.TaskUpdateDto dto)
        {

            try
            {
                var sender = new RabbitSender(_configuration["Rabbit:Host"], _configuration["Rabbit:User"], _configuration["Rabbit:Password"]);
                var message = new OperationMessage();
                message.MessageId = Guid.NewGuid().ToString();
                message.OperationType = OperationType.Update;
                message.Message = dto.ToString();

                sender.QueueMessage(message, "operationQueue");
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao tentar atualizar tarefa", ex);
                return new StatusCodeResult(500);
            }

            return Accepted();
        }
        [HttpGet]
        [Route("/api/[controller]/Filter")]
        public async Task<string> Filter(string? description, int? taskStatusId, DateTime? date)
        {
            var urlSearch = $"{_configuration["UrlApiSearch"]}/api/task/filter?description={description}&taskStatusId={taskStatusId}&date={date}";
            return await Get(urlSearch);
        }
            

        private async Task<string> Get(string url)
        {
            var response = await RequestApi(url, HttpMethod.Get);
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<HttpResponseMessage> RequestApi(string uri, HttpMethod method)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using var client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response;


            if (method == HttpMethod.Post)
                response = await client.PostAsync(uri, null);
            else if (method == HttpMethod.Get)
                response = await client.GetAsync(uri);
            else if (method == HttpMethod.Patch)
                response = await client.PatchAsync(uri, null);
            else
                throw new NotImplementedException();

            return response;
        }
    }
}
