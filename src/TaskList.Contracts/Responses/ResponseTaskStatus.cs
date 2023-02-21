namespace TaskList.Contracts.Responses;

public record ResponseTaskStatus(Guid Id, string Name)
{
    private static readonly ResponseTaskStatus Waiting = new(new Guid("FE50A03B-0F5B-405E-B7FD-31712DB5E86A"), "Waiting");
    private static readonly ResponseTaskStatus InWork = new(new Guid("280B59E2-5BD3-45E5-822C-011CD96191FE"), "In work");
    private static readonly ResponseTaskStatus Completed = new(new Guid("280B59E2-5BD3-45E5-822C-011CD96191FE"), "Completed");
    
    public static ResponseTaskStatus Default => Waiting;

    public static readonly ResponseTaskStatus[] Defaults =
    {
        Waiting, InWork, Completed
    };
}