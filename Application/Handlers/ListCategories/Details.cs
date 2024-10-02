using Application.Core;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.ListCategories {
    public class Details {
        public class Query: IRequest<Result<ListCategory>> {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ListCategory>> {

            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<ListCategory>> Handle(Query request, CancellationToken cancellationToken)
            {
                var category = await _context.ListCategories.FirstOrDefaultAsync(c => c.Id == request.Id);

                return Result<ListCategory>.Success(category);
            }
        }
    }
}