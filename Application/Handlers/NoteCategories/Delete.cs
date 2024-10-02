using Application.Core;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.NoteCategories {
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
                NoteCategory category = await _context.NoteCategories
                    .Include(n => n.Notes)
                    .FirstOrDefaultAsync(c => c.Id == request.Id);

                if (category == null) return null;
                
                if(category.Notes.Count > 0) return Result<Unit>.Failure("Cannot delete a category with existing notes.");
                
                _context.NoteCategories.Remove(category);

                var result = await _context.SaveChangesAsync() > 0;
                
                if(!result) return Result<Unit>.Failure("Failure to delete category.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}