using AutoMapper;
using B3Challenge.Business;
using B3Challenge.Business.Interfaces;
using B3Challenge.Domain.Dtos.Task;
using B3Challenge.Rabbit;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace B3Challenge.ReadOnly.API.Controllers
{
    public class TaskController : Controller
    {

        private readonly ILogger<TaskController> _logger;
        private readonly IMapper _mapper;

        private readonly ITaskBusiness _taskBusiness;

        public TaskController(ILogger<TaskController> logger, IMapper mapper, ITaskBusiness taskBusiness)
        {
            _logger = logger;
            _mapper= mapper;

            _taskBusiness = taskBusiness;
        }



        [HttpGet]
        [Route("/api/Filter")]
        public IActionResult Filter(int? id, string? description, int? taskStatusId)
        {
            try
            {
                var list = _taskBusiness.Filter(new Domain.Dtos.Task.TaskFilterDto(id, description, taskStatusId));
                return Json(_mapper.Map<List<TaskResponseFilterDto>>(list));
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao tentar filtrar tarefas", ex);
                return new StatusCodeResult(500);
            }
        }

    }
}
