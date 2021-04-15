using System.Collections.Generic;
using AppGmz.Models.DomainModels;
using AppGmz.Models.DtoModels;
using AppGmz.Models.DtoModels.VacanciesDTO;
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
            CreateMap<RecordNews, FullRecordNewsDto>();
            CreateMap<IEnumerable<RecordNews>, IEnumerable<FindRecordNewsDto>>();
            CreateMap<UserRegisterDto, AppUser>();
            CreateMap<CreateVacanciesDto, Vacancies>();
            CreateMap<Vacancies, CreateVacanciesDto>();
            CreateMap<Vacancies, DetailVacanciesDto>();
            CreateMap<IEnumerable<Vacancies>, IEnumerable<ShowVacanciesDto>>();
        }
    }
}