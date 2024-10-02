using Application.Core;
using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using MediatR;
using Persistence;
using Task = System.Threading.Tasks.Task;

namespace Application.Handlers.ListCategories {
    public class Edit {
        public class Command : IRequest<Result<Unit>> {
            public CategoryDto Category { get; set; }
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
                ListCategory category = await _context.ListCategories.FindAsync(request.Category.Id);

                if (category == null) return null;

                _mapper.Map(request.Category, category);
                
                var result =  await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to save changes to category.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}