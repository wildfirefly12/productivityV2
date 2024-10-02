
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers {

    public class TagsController : BaseApiController {
        private DataContext _context;

        public TagsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Tag>>> ByUser(string id)
        {
            List<Tag> tags = await _context.Tags
                .Where(t => t.UserId == id)
                .ToListAsync();

            return tags;
        }

        /*[HttpGet]
        public async Task<ActionResult<List<Tag>>> ByTask(long id)
        {
            Models.Task task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);

            List<Tag> tags = await _context.Tags
                .Include(t => t.Tasks)
                .Where(t => t.Tasks.Contains(task))
                .ToListAsync<Tag>();

            return tags;
        }

        [HttpGet]
        public async Task<ActionResult<List<Tag>>> ByNote(long id)
        {
            Note note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);

            List<Tag> tags = await _context.Tags
                .Where(t => t.Notes.Contains(note))
                .ToListAsync();

            return tags;
        }

        [HttpGet]
        public async Task<ActionResult<List<Tag>>> ByList(long id)
        {
            List list = await _context.Lists.FirstOrDefaultAsync(l => l.Id == id);

            List<Tag> tags = await _context.Tags
                .Where(t => t.Lists.Contains(list))
                .ToListAsync();

            return tags;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TagDto tagDto)
        {
            Tag tag = new Tag(tagDto);

            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromBody] TagDto tagDto)
        {
            Tag tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == tagDto.Id);

            tag.Description = tagDto.Description;
            tag.Color = tagDto.Color;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(long id)
        {
            Tag tag = await _context.Tags
                .Include(t => t.Tasks)
                .Include(t => t.Notes)
                .Include(t => t.Lists)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tag.Tasks.Count == 0 && tag.Notes.Count == 0 && tag.Lists.Count == 0)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();

                return Ok();
            }

            return BadRequest("Cannot delete active tag.");
        }

        [HttpPost]
        public async Task<ActionResult> TagObject(long tagId, long? taskId, long? noteId, long? listId)
        {
            Tag tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == tagId);

            if (taskId != null)
            {
                Models.Task task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);

                tag.Tasks.Add(task);
            }

            if (noteId != null)
            {
                Note note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == noteId);

                tag.Notes.Add(note);
            }

            if (listId != null)
            {
                List list = await _context.Lists.FirstOrDefaultAsync(l => l.Id == listId);

                tag.Lists.Add(list);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> RemoveFromObject(IFormCollection data)
        {
            long tagId;
            long.TryParse(data["tagId"], out tagId);
            long taskId;
            long.TryParse(data["taskId"], out taskId);
            long noteId;
            long.TryParse(data["noteId"], out noteId);
            long listId;
            long.TryParse(data["listId"], out listId);


            Tag tag = await _context.Tags
                .Include(t => t.Tasks)
                .Include(t => t.Notes)
                .Include(t => t.Lists)
                .FirstOrDefaultAsync(t => t.Id == tagId);

            if (taskId != 0)
            {
                Models.Task task = await _context.Tasks
                    .Include(t => t.Tags)
                    .FirstOrDefaultAsync(t => t.Id == taskId);

                task.Tags.Remove(tag);
                tag.Tasks.Remove(task);
            }

            if (noteId != 0)
            {
                Note note = await _context.Notes
                    .Include(n => n.Tags)
                    .FirstOrDefaultAsync(n => n.Id == noteId);

                note.Tags.Remove(tag);
                tag.Notes.Remove(note);
            }

            if (listId != 0)
            {
                List list = await _context.Lists
                    .Include(l => l.Tags)
                    .FirstOrDefaultAsync(l => l.Id == listId);

                list.Tags.Remove(tag);
                tag.Lists.Remove(list);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }*/
    }
}