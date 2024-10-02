using Application.Core;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.Tasks {
    public class Details {
        public class Query: IRequest<Result<TaskItem>> {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<TaskItem>> {

            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<TaskItem>> Handle(Query request, CancellationToken cancellationToken)
            {
                var task = await _context.Tasks
                    .FirstOrDefaultAsync(l => l.Id == request.Id);

                return Result<TaskItem>.Success(task);
            }
        }
    }
}