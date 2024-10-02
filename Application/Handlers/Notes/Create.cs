using Application.Core;
using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using MediatR;
using Persistence;

namespace Application.Handlers.Notes {
    public class Create {
        public class Command : IRequest<Result<Unit>> {
            public NoteDto Note { get; set; }
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
                _context.Notes.Add(_mapper.Map<Note>(request.Note));

                var result = await _context.SaveChangesAsync() > 0;
                
                if(!result) return Result<Unit>.Failure("Failed to create note.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}