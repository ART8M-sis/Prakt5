using Tracker.DTO;

namespace Tracker.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItemDto>> GetAllAsync(Guid? projectId  = null);
        Task<TaskItemDto> GetByIdAsync(Guid id);
        Task<TaskItemDto> CreateAsync(CreateTaskItemDto dto);
        Task<TaskItemDto> UpdateAsync(Guid id, UpdateTaskItemDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
