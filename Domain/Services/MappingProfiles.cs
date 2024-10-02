using AutoMapper;
using Domain.Dtos;
using Domain.Models;

namespace Domain.Services;

public class MappingProfiles : Profile {
    public MappingProfiles()
    {
        CreateMap<ItemList, ListDto>();
        CreateMap<ListCategory, CategoryDto>();
        CreateMap<ListItem, ListItemDto>();
        CreateMap<Note, NoteDto>();
        CreateMap<Tag, TagDto>();
        CreateMap<TaskItem, TaskDto>();
        CreateMap<User, UserDto>();
    }
}