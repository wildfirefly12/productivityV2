﻿using Application.Core;
using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using MediatR;
using Persistence;

namespace Application.Handlers.Notes {
    public class Edit {
        public class Command : IRequest<Result<Unit>> {
            public NoteDto Note { get; set; }
        }

        public class Handler : IRequestHandler<Edit.Command, Result<Unit>> {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Edit.Command request, CancellationToken cancellationToken)
            {
                Note note = await _context.Notes.FindAsync(request.Note.Id);

                if (note == null) return null;

                _mapper.Map(note, request.Note);
                
                var result = await _context.SaveChangesAsync() > 0;
                
                if (!result) return Result<Unit>.Failure("Failed to save changes to note.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}