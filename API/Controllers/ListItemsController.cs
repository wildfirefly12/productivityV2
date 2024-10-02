using Microsoft.AspNetCore.Mvc;
using Application.Handlers.ListItems;
using Domain.Dtos;


namespace API.Controllers;

public class ListItemsController : BaseApiController{

    [HttpGet]
    public async Task<IActionResult> ByList(long id)
    {
        return HandleResult(await Mediator.Send(new List.Query{ListId = id}));
    }
        
    [HttpGet]
    public async Task<IActionResult> ById(long id)
    {
        return HandleResult(await Mediator.Send(new Details.Query{Id = id}));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ListItemDto listItem)
    {
        return HandleResult(await Mediator.Send(new Create.Command { ListItem = listItem }));
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] ListItemDto listItem)
    {
        return HandleResult(await Mediator.Send(new Edit.Command { ListItem = listItem }));
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] long id)
    {
        return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
    }
}