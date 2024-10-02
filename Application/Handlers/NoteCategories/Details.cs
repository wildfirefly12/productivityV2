using Application.Core;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.NoteCategories {
    public class Details {
        public class Query: IRequest<Result<NoteCategory>> {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<NoteCategory>> {

            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<NoteCategory>> Handle(Query request, CancellationToken cancellationToken)
            {
                var category = await _context.NoteCategories.FirstOrDefaultAsync(c => c.Id == request.Id);

                return Result<NoteCategory>.Success(category);
            }
        }
    }
}