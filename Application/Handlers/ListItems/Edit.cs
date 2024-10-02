using Application.Core;
using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using MediatR;
using Persistence;
using Task = System.Threading.Tasks.Task;

namespace Application.Handlers.ListItems {
    public class Edit {
        public class Command : IRequest<Result<Unit>> {
            public ListItemDto ListItem { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>> {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                ListItem item = await _context.ListItems.FindAsync(request.ListItem.Id);

                if (item == null) return null;

                _mapper.Map(item, request.ListItem);
                
                var result =  await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to save changes to item.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}