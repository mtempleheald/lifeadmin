using LifeAdmin.ComponentModels;
using Microsoft.Extensions.Logging;
using SQLite;

namespace LifeAdmin.Persistence;

public class LifeAdminDatabase(ILogger<LifeAdminDatabase> logger)
{
    private SQLiteAsyncConnection _database = null!;

    async Task Init()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (_database is not null)
        {
            var cols = (await _database.GetTableInfoAsync("Tasks"))
                .Select(ci => ci.ToString())
                .Aggregate((current, next) => current + ", " + next);
            logger.LogInformation("Columns on table Tasks {cols}", cols);
            return;
        }

        _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var taskDetailTableResult = await _database.CreateTableAsync<TaskDetailEntity>();
        logger.LogInformation("TaskDetail table result = {TableCreationResult}", taskDetailTableResult);
    }
    
    public async Task<List<TaskDetailEntity>> GetTasksAsync()
    {
        await Init();
        return await _database.Table<TaskDetailEntity>()
            .ToListAsync();
    }

    public async Task<TaskDetailEntity> GetItemAsync(int id)
    {
        await Init();
        return await _database.Table<TaskDetailEntity>()
            .Where(i => i.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(TaskDetailEntity item)
    {
        await Init();
        if (item.Id != 0)
            return await _database.UpdateAsync(item);
        else
            return await _database.InsertAsync(item);
    }

    public async Task<int> DeleteItemAsync(TaskDetailEntity item)
    {
        await Init();
        return await _database.DeleteAsync(item);
    }
}