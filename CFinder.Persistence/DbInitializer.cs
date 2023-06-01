namespace CFinder.Persistence;

public static class DbInitializer
{
    public static void Init(DataStore context)
    {
        context.Database.EnsureCreated();
    }
}