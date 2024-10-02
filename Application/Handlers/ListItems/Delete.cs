using Application.Core;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Task = System.Threading.Tasks.Task;

namespace Application.Handlers.ListItems {
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
                ListItem item = await _context.ListItems
                    .FirstOrDefaultAsync(c => c.Id == request.Id);

                if (item == null) return null;
                
                _context.ListItems.Remove(item);

                var result = await _context.SaveChangesAsync() > 0;
                
                if(!result) return Result<Unit>.Failure("Failure to delete item.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}