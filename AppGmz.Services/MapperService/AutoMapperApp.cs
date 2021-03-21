using System.Collections.Generic;
using AppGmz.Models.DomainModels;
using AppGmz.Models.DtoModels;
using AppGmz.Models.IdentityModels;
using AutoMapper;

namespace AppGmz.Services.MapperService
{
    public class AutoMapperApp : Profile
    {
        public AutoMapperApp()
        {
            CreateMap<CreateRecordNewsDto, RecordNews>();
            CreateMap<RecordNews, CreateRecordNewsDto>();
            CreateMap<RecordNews, FoundRecordNewsDto>();
            CreateMap<IEnumerable<RecordNews>, IEnumerable<FindRecordNewsDto>>();
            CreateMap<UserRegisterDto, AppUser>();
        }
    }
}