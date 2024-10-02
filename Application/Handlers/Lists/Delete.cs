using Application.Core;
using Domain.Models;
using MediatR;
using Persistence;

namespace Application.Handlers.Lists {
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
                ItemList list = await _context.Lists.FindAsync(request.Id);

                if (list == null) return null;

                _context.Lists.Remove(list);
                
                var result = await _context.SaveChangesAsync() > 0;
                
                if (!result) return Result<Unit>.Failure("Failed to delete to list.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}