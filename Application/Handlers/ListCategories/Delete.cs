using Application.Core;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.ListCategories {
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
                ListCategory category = await _context.ListCategories
                    .Include(n => n.Lists)
                    .FirstOrDefaultAsync(c => c.Id == request.Id);

                if (category == null) return null;
                
                if(category.Lists.Count > 0) return Result<Unit>.Failure("Cannot delete a category with existing lists.");
                
                _context.ListCategories.Remove(category);

                var result = await _context.SaveChangesAsync() > 0;
                
                if(!result) return Result<Unit>.Failure("Failure to delete category.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}