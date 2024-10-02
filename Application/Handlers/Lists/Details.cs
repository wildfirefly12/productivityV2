using Application.Core;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.Lists {
    public class Details {
        public class Query: IRequest<Result<ItemList>> {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ItemList>> {

            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<ItemList>> Handle(Query request, CancellationToken cancellationToken)
            {
                var list = await _context.Lists
                    .Include(l => l.Items)
                    .FirstOrDefaultAsync(l => l.Id == request.Id);

                return Result<ItemList>.Success(list);
            }
        }
    }
}