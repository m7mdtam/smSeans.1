public interface ITaskRepository
{
    Task<Task> GetTaskByIdAsync(int id);
    Task<int> CreateTaskAsync(Task task); // Returns the created task's ID
    Task<bool> UpdateTaskAsync(Task task); // Returns success status
    Task<bool> DeleteTaskAsync(int id); // Returns success status
}
