using Application.Core;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Handlers.ListItems {
    public class Details {
        public class Query: IRequest<Result<ListItem>> {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ListItem>> {

            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<ListItem>> Handle(Query request, CancellationToken cancellationToken)
            {
                var item = await _context.ListItems.FirstOrDefaultAsync(c => c.Id == request.Id);

                return Result<ListItem>.Success(item);
            }
        }
    }
}