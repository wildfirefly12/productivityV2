using Application.Core;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.Notes {
    public class Details {
        public class Query: IRequest<Result<Note>> {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Note>> {

            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Note>> Handle(Query request, CancellationToken cancellationToken)
            {
                var note = await _context.Notes.FirstOrDefaultAsync(c => c.Id == request.Id);

                return Result<Note>.Success(note);
            }
        }
    }
}