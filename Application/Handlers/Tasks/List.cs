using Application.Core;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.Tasks {
    public class List {
        public class Query: IRequest<Result<List<TaskItem>>> {
            public string UserId { get; set; }
            public string Type { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<TaskItem>>> {

            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<TaskItem>>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<TaskItem> tasks = new List<TaskItem>();
                
                switch (request.Type)
                {
                    case "today":
                        tasks = await _context.Tasks.Where(t => t.UserId == request.UserId && t.DueDate.Date == DateTime.Today.Date).ToListAsync();
                        break;
                    case "pending":
                        tasks = await _context.Tasks.Where(t => t.UserId == request.UserId && !t.IsComplete).ToListAsync();
                        break;
                    case "overdue":
                        tasks = await _context.Tasks.Where(t => t.UserId == request.UserId && !t.IsComplete && t.DueDate < DateTime.Now).ToListAsync();
                        break;
                    case "completed":
                        tasks = await _context.Tasks.Where(t => t.UserId == request.UserId && t.IsComplete).ToListAsync();
                        break;
                    case "recurring":
                        tasks = await _context.Tasks.Where(t => t.UserId == request.UserId && t.IsRecurring).ToListAsync();
                        break;
                    default:
                        tasks = await _context.Tasks.Where(t => t.UserId == request.UserId).ToListAsync();
                        break;
                }

                return Result<List<TaskItem>>.Success(tasks);
            }
        }
    }
}