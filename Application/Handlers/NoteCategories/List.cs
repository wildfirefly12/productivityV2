using Application.Core;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.NoteCategories {
    public class List {
        public class Query: IRequest<Result<List<NoteCategory>>> {
            public string UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<NoteCategory>>> {

            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<NoteCategory>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<List<NoteCategory>>.Success(await _context.NoteCategories
                    .Where(c => c.UserId == request.UserId).ToListAsync());
            }
        }
    }
}