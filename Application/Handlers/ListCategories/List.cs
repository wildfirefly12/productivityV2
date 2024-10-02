using Application.Core;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.ListCategories {
    public class List {
        public class Query: IRequest<Result<List<ListCategory>>> {
            public string UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<ListCategory>>> {

            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<ListCategory>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<List<ListCategory>>.Success(await _context.ListCategories
                    .Where(c => c.UserId == request.UserId).ToListAsync());
            }
        }
    }
}