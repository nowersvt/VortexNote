using AutoMapper;
using VortextNote.Domain.Entities;
using VortextNote.Domain.ViewModels;

namespace VortexNote.Application.Common.Mappers
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Note, NoteViewModel>();
            CreateMap<Chunk, ChunkViewModel>();
            CreateMap<User, UserViewModel>();
        }
    }
}