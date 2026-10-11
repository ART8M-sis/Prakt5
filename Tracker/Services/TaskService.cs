using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tracker.Data;
using Tracker.DTO;
using Tracker.Models;

namespace Tracker.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TaskService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskItemDto>> GetAllAsync(Guid? projectId = null)
        {
            var query = _context.TaskItems.AsNoTracking().AsQueryable();
            if (projectId.HasValue)
            {
                query = query.Where(t => t.ProjectId == projectId.Value);
            }
            var tasks = await query.ToListAsync();
            return _mapper.Map<IEnumerable<TaskItemDto>>(tasks);
        }

        public async Task<TaskItemDto> GetByIdAsync(Guid id)
        {
            var task = await _context.TaskItems
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {id} not found.");
            }
            return _mapper.Map<TaskItemDto>(task);
        }

        public async Task<TaskItemDto> CreateAsync(CreateTaskItemDto dto)
        {
            var projectExists = await _context.Projects.AnyAsync(p => p.Id == dto.ProjectId);
            if (!projectExists)
                throw new KeyNotFoundException($"Project with ID {dto.ProjectId} not found.");

            var task = _mapper.Map<TaskItem>(dto);
            task.Id = Guid.NewGuid();
            task.CreatedAt = DateTime.UtcNow;
            task.IsDeleted = false;

            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();
            return _mapper.Map<TaskItemDto>(task);
        }

        public async Task<TaskItemDto> UpdateAsync(Guid id, UpdateTaskItemDto dto)
        {
            var task = await _context.TaskItems
                .FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {id} not found.");
            }
            _mapper.Map(dto, task);
            await _context.SaveChangesAsync();
            return _mapper.Map<TaskItemDto>(task);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var task = await _context.TaskItems
                .FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                return false;
            }
            task.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
