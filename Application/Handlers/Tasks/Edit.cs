using Application.Core;
using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using MediatR;
using Persistence;

namespace Application.Handlers.Tasks {
    public class Edit {
        public class Command : IRequest<Result<Unit>> {
            public TaskDto Task { get; set; }
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
                TaskItem task = await _context.Tasks.FindAsync(request.Task.Id);

                if (task == null) return null;

                _mapper.Map(task, request.Task);
                
                
                var result = await _context.SaveChangesAsync() > 0;
                
                if (!result) return Result<Unit>.Failure("Failed to save changes to task.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}