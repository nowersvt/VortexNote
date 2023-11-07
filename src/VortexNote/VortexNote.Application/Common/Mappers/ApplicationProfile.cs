using AutoMapper;
using VortexNote.Domain.Base.Files;
using VortexNote.Domain.Entities;
using VortexNote.Domain.ViewModels;

namespace VortexNote.Application.Common.Mappers
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Note, NoteViewModel>();
            CreateMap<Chunk, ChunkViewModel>();
            CreateMap<User, UserViewModel>();
            CreateMap<User, SavedData>()
                .ForMember(x=>x.UserId,o=>o.MapFrom(s=>s.Id));
        }
    }
}