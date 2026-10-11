using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tracker.Data;
using Tracker.DTO;
using Tracker.Models;

namespace Tracker.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProjectService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var projects = await _context.Projects
                .AsNoTracking()
                .ToListAsync();
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task<ProjectDto> GetByIdAsync(Guid id)
        {
            var project = await _context.Projects.
                AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found.");
            }
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<ProjectDto> CreateAsync(CreateProjectDto dto)
        {
            var project = _mapper.Map<Project>(dto);
            project.Id = Guid.NewGuid();
            project.CreatedAt = DateTime.UtcNow;
            project.IsDeleted = false;   

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<ProjectDto> UpdateAsync(Guid id, UpdateProjectDto dto)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found.");
            }
            _mapper.Map(dto, project);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (project == null)
            {
                return false;
            }
            project.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
