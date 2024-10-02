using Application.Handlers.Tasks;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class TasksController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<TaskItem>>> ByUserType(string id, string type)
        {
            return HandleResult(await Mediator.Send(new List.Query{UserId = id, Type = type}));
        }
        
        [HttpGet]
        public async Task<ActionResult<TaskItem>> ById(long id)
        {
            return HandleResult(await Mediator.Send(new Details.Query{Id = id}));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskDto task)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Task = task }));
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] TaskDto task)
        {
            return HandleResult(await Mediator.Send(new Edit.Command { Task = task }));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] long id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}