using AutoMapper;
using System.Linq;
using TheaterApplication.Bll.Models;
using TheaterApplication.Dal.DbModels;
using TheaterApplication.WebApi.PostModels;
using TheaterApplication.WebApi.ViewModels;

namespace TheaterApplication.WebApi.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDbModel, UserWithTokenData>().
                ForMember(dst => dst.Token,
                    src => src.MapFrom(x => x.Tokens != null && x.Tokens.Count > 0
                        ? x.Tokens.First().Token : null
                    )
                ).
                ForMember(dst => dst.Roles,
                    src => src.MapFrom(x => x.UserRoles != null
                        ? x.UserRoles.Select(x => x.Role.Name).ToArray() : new string[0]
                    )
                );
            CreateMap<UserDbModel, User>();
            CreateMap<UserWithTokenData, UserVm>();
            CreateMap<User, UserVm>();

            CreateMap<PerformancePm, Performance>();
            CreateMap<Performance, PerformanceDbModel>();
            CreateMap<PerformanceDbModel, Performance>();
            CreateMap<Performance, PerformanceVm>();

            CreateMap<PerformanceSchedulePm, PerformanceSchedule>();
            CreateMap<PerformanceSchedule, PerformanceScheduleDbModel>();
            CreateMap<PerformanceScheduleDbModel, PerformanceSchedule>();
            CreateMap<PerformanceSchedule, PerformanceScheduleVm>();

            CreateMap<PerformancePosterPm, PerformancePoster>();
            CreateMap<PerformancePoster, PerformancePosterDbModel>();
            CreateMap<PerformancePosterDbModel, PerformancePoster>();
            CreateMap<PerformancePoster, PerformancePosterVm>();
        }
    }
}
