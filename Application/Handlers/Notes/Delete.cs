using Application.Core;
using Domain.Models;
using MediatR;
using Persistence;

namespace Application.Handlers.Notes {
    public class Delete {
        public class Command : IRequest<Result<Unit>> {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Delete.Command, Result<Unit>> {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Delete.Command request, CancellationToken cancellationToken)
            {
                Note note = await _context.Notes.FindAsync(request.Id);

                if (note == null) return null;

                _context.Notes.Remove(note);
                
                var result = await _context.SaveChangesAsync() > 0;
                
                if (!result) return Result<Unit>.Failure("Failed to delete to note.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}