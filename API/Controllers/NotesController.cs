using Application.Handlers.Notes;
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Domain.Models;

namespace API.Controllers
{

    public class NotesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Note>>> ByCategory(long id)
        {
            return HandleResult(await Mediator.Send(new List.Query{CategoryId = id}));
        }
        
        [HttpGet]
        public async Task<ActionResult<Note>> ById(long id)
        {
            return HandleResult(await Mediator.Send(new Details.Query{Id = id}));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NoteDto note)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Note = note }));
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] NoteDto note)
        {
            return HandleResult(await Mediator.Send(new Edit.Command { Note = note }));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] long id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}