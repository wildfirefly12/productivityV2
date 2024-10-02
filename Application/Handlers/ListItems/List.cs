using Application.Core;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.ListItems {
    public class List {
        public class Query: IRequest<Result<List<ListItem>>> {
            public long ListId{ get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<ListItem>>> {

            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<ListItem>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<List<ListItem>>.Success(await _context.ListItems
                    .Where(l => l.ListId == request.ListId).ToListAsync());
            }
        }
    }
}