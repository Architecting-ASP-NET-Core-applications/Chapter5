namespace Chapter5Routes;


public static class MinimalApi
{
    private static readonly List<TaskItem> Tasks = Enumerable.Range(1, 50)
    .Select(i => new TaskItem
    {
        Id = i,
        Title = $"Task {i}",
        Description = $"Description for Task {i}",
        IsCompleted = i % 2 == 0
    })
    .ToList();

    public static IEndpointRouteBuilder UseMinimalApi(this IEndpointRouteBuilder app)
    {
        _=app.MapGroup("/public/tasklist")
         .MapTaskListApi();
        return app;
    }

    static RouteGroupBuilder MapTaskListApi(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAll);
        group.MapGet("/{id}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id}", Update);
        group.MapDelete("/{id}", Delete);

        return group;
    }


    static IResult GetAll()
    {
        return Results.Ok(Tasks);
    }

    static IResult GetById(int id)
    {
        var task = Tasks.Find(t => t.Id == id);
        return task != null ? Results.Ok(task) : Results.NotFound(new { Message = "Task not found" });
    }

    static IResult Create(TaskItem newTask)
    {
        newTask.Id = Tasks.Count > 0 ? Tasks[^1].Id + 1 : 1; // Generate a new ID
        Tasks.Add(newTask);
        return Results.Created($"/tasks/{newTask.Id}", newTask);
    }

    static IResult Update(int id, TaskItem updatedTask)
    {
        var existingTask = Tasks.Find(t => t.Id == id);
        if (existingTask == null)
        {
            return Results.NotFound(new { Message = "Task not found" });
        }

        existingTask.Title = updatedTask.Title;
        existingTask.Description = updatedTask.Description;
        existingTask.IsCompleted = updatedTask.IsCompleted;

        return Results.Ok(existingTask);
    }

    static IResult Delete(int id)
    {
        var task = Tasks.Find(t => t.Id == id);
        if (task == null)
        {
            return Results.NotFound(new { Message = "Task not found" });
        }

        Tasks.Remove(task);
        return Results.NoContent();
    }

}

