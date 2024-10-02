using Application.Core;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.Lists {
    public class List {
        public class Query: IRequest<Result<List<ItemList>>> {
            public long CategoryId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<ItemList>>> {

            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<ItemList>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var lists = await _context.Lists.Where(n => n.CategoryId == request.CategoryId).ToListAsync();

                return Result<List<ItemList>>.Success(lists);
            }
        }
    }
}