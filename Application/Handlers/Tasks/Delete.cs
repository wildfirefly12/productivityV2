using Application.Core;
using Domain.Models;
using MediatR;
using Persistence;

namespace Application.Handlers.Tasks {
    public class Delete {
        public class Command : IRequest<Result<Unit>> {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>> {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                TaskItem task = await _context.Tasks.FindAsync(request.Id);

                if (task == null) return null;

                _context.Tasks.Remove(task);
                
                var result = await _context.SaveChangesAsync() > 0;
                
                if (!result) return Result<Unit>.Failure("Failed to delete to task.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}