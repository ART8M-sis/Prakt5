using AutoMapper;
using Tracker.DTO;
using Tracker.Models;

namespace Tracker.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<CreateProjectDto, Project>();
            CreateMap<UpdateProjectDto, Project>();
            
            CreateMap<TaskItem, TaskItemDto>();
            CreateMap<CreateTaskItemDto, TaskItem>();
            CreateMap<UpdateTaskItemDto, TaskItem>();
            
        }
    }
}
